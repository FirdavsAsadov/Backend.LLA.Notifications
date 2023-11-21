using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Options;
using Notifications.Infrastructure.Application.Common.Notifications.Models;
using Notifications.Infrastructure.Application.Common.Notifications.Services;
using Notifications.Infrastructure.Infrastructure.Common.Settings;

namespace Notifications.Infrastructure.Infrastructure.Common.Notifications.Services;

public class SmsRenderingService : ISmsRenderingService
{
    private readonly TemplateRendiringSettings _templateRenderingSettings;

    public SmsRenderingService(IOptions<TemplateRendiringSettings> templateRenderingSettings)
    {
        _templateRenderingSettings = templateRenderingSettings.Value;
    }
    
    public ValueTask<string> RenderAsync(SmsMessage smsMessage, CancellationToken cancellationToken = default)
    {
        var placeHolderRegex = new Regex(_templateRenderingSettings.PlaceholderRegexPattern,
            RegexOptions.Compiled,
            TimeSpan.FromSeconds(_templateRenderingSettings.RegexMatchTimeoutInSeconds));

        var placeHolderRegexValue = new Regex(_templateRenderingSettings.PlaceholderValueRegexPattern,
            RegexOptions.Compiled,
            TimeSpan.FromSeconds(_templateRenderingSettings.RegexMatchTimeoutInSeconds));

        var matches = placeHolderRegex.Matches(smsMessage.Template.Content);

        if (matches.Any() && !smsMessage.Variables.Any())
            throw new InvalidOperationException("Variables are required for this template.");

        var templatePlaceholders = matches.Select(match =>
            {
                var placeholder = match.Value;
                var placeholderValue = placeHolderRegexValue.Match(placeholder).Groups[1].Value;
                var valid = smsMessage.Variables.TryGetValue(placeholderValue, out var value);

                return new TemplatePlaceHolder
                {
                    Placeholder = placeholder,
                    PlaceholderValue = placeholderValue,
                    Value = value,
                    IsValid = valid
                };
            })
            .ToList();
        
        ValidatePlaceholders(templatePlaceholders);

        var messageBuilder = new StringBuilder(smsMessage.Template.Content);
        templatePlaceholders.ForEach(placeholder => messageBuilder.Replace(placeholder.Placeholder, placeholder.Value));
        
        var message = messageBuilder.ToString();
        smsMessage.Message = message;

        return ValueTask.FromResult(message);
    }
        private void ValidatePlaceholders(IEnumerable<TemplatePlaceHolder> templatePlaceholders)
        {
            var missingPlaceholders = templatePlaceholders.Where(placeholder => !placeholder.IsValid)
                .Select(placeholder => placeholder.PlaceholderValue)
                .ToList();

            if (!missingPlaceholders.Any()) return;

            var errorMessage = new StringBuilder();
            missingPlaceholders.ForEach(placeholderValue => errorMessage.Append(placeholderValue).Append(','));

            throw new InvalidOperationException(
                $"Variable for given placeholders is not found - {string.Join(',', missingPlaceholders)}");
        }
}