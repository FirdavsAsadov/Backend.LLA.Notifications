using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Options;
using Notifications.Infrastructure.Application.Common.Notifications.Models;
using Notifications.Infrastructure.Application.Common.Notifications.Services;
using Notifications.Infrastructure.Infrastructure.Common.Settings;

namespace Notifications.Infrastructure.Infrastructure.Common.Notifications.Services;

public class EmailRendiringService : IEmailRenderingService
{
    private readonly TemplateRendiringSettings _templateRenderingSettings;

    public EmailRendiringService(IOptions<TemplateRendiringSettings> templateRenderingSettings)
    {
        _templateRenderingSettings = templateRenderingSettings.Value;
    }
    public ValueTask<string> RenderAsync(EmailMessage emailMessage, CancellationToken cancellationToken = default)
    {
        var placeholderRegex = new Regex(_templateRenderingSettings.PlaceholderRegexPattern,
            RegexOptions.Compiled,
            TimeSpan.FromSeconds(_templateRenderingSettings.RegexMatchTimeoutInSeconds));

        var placeholderValueRegex = new Regex(_templateRenderingSettings.PlaceholderValueRegexPattern,
            RegexOptions.Compiled,
            TimeSpan.FromSeconds(_templateRenderingSettings.RegexMatchTimeoutInSeconds));

        var matches = placeholderRegex.Matches(emailMessage.Template.Content);

        if (matches.Any() && !emailMessage.Variables.Any())
            throw new InvalidOperationException("Variables are required for this template.");

        var templatePLaceholders = matches.Select(match =>
        {
            var placeholder = match.Value;
            var placeholderValue = placeholderValueRegex.Match(placeholder).Groups[1].Value;
            var valid = emailMessage.Variables.TryGetValue(placeholderValue, out var value);

            return new TemplatePlaceHolder
            {
                Placeholder = placeholder,
                PlaceholderValue = placeholderValue,
                Value = value,
                IsValid = valid
            };
        }).ToList();
        
        ValidatePlaceholder(templatePLaceholders);
        
        var messageBuilder = new StringBuilder(emailMessage.Template.Content);
        templatePLaceholders.ForEach(placeholder => messageBuilder.Replace(placeholder.Placeholder, placeholder.Value));
        
        var message = messageBuilder.ToString();
        emailMessage.Body = message;
        emailMessage.Subject = emailMessage.Template.Subject;

        return ValueTask.FromResult(message);
    }

    private void ValidatePlaceholder(IEnumerable<TemplatePlaceHolder> templatePlaceHolders)
    {
        var missingPlaceholder = templatePlaceHolders.Where(placeholder => !placeholder.IsValid)
            .Select(placeholder => placeholder.PlaceholderValue)
            .ToList();
        
        if(!missingPlaceholder.Any()) return;

        var errorMessage = new StringBuilder();
        missingPlaceholder.ForEach(placeholder => errorMessage.Append(placeholder).Append(','));

        throw new InvalidOperationException(
            $"Variable for given placeholder is not found - {string.Join(',', missingPlaceholder)}");
    }
}