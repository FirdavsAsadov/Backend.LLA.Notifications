using FluentValidation;
using Notifications.Infrastructure.Domain.Entiteis;
using Notifications.Infrastructure.Domain.Enums;

namespace Notifications.Infrastructure.Infrastructure.Common.Validators;

public class EmailTemplateValidator : AbstractValidator<EmailTemplate>
{
    public EmailTemplateValidator()
    {
        RuleFor(template => template.Content)
            .NotEmpty()
            .MinimumLength(10)
            .MaximumLength(256);

        RuleFor(template => template.Type)
            .Equal(NotificationType.Email);
    }
}