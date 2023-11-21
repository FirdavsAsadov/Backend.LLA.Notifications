using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Notifications.Infrastructure.Application.Common.Models.Querying;
using Notifications.Infrastructure.Application.Common.Notifications.Services;
using Notifications.Infrastructure.Application.Common.Querying.Extensions;
using Notifications.Infrastructure.Domain.Entiteis;
using Notifications.Infrastructure.Domain.Enums;
using Notifications.Infrastructure.Persistence.Repositories.Interfaces;
using ValidationException = FluentValidation.ValidationException;

namespace Notifications.Infrastructure.Infrastructure.Common.Notifications.Services;

public class SmsTemplateService : ISmsTemplateService
{
    private readonly ISmsTemplateRepository _smsTemplateRepository;
    private readonly IValidator<SmsTemplate> _smsTemplateValidator;

    public SmsTemplateService(ISmsTemplateRepository smsTemplateRepository, IValidator<SmsTemplate> smsTemplateValidator)
    {
        _smsTemplateValidator = smsTemplateValidator;
        _smsTemplateRepository = smsTemplateRepository;
    }
    public IQueryable<SmsTemplate> Get(
        Expression<Func<SmsTemplate, bool>>? predicate = default,
        bool asNoTracking = false
        ) => _smsTemplateRepository.Get(predicate, asNoTracking);
    
    public async ValueTask<IList<SmsTemplate>> GetByFilterAsync(FilterPagination paginationOptions, bool asNoTracking = false,
        CancellationToken cancellationToken = default)
    {
        return await Get(asNoTracking: asNoTracking)
            .ApplyPagination(paginationOptions)
            .ToListAsync(cancellationToken: cancellationToken);
    }

    public async ValueTask<SmsTemplate?> GetByTypeAsync(NotificationTemplateType templateType, bool asNoTracking = false,
        CancellationToken cancellationToken = default)
    {
        return await _smsTemplateRepository.Get(template => template.TemplateType == templateType, asNoTracking)
            .SingleOrDefaultAsync(cancellationToken);
    }

    public ValueTask<SmsTemplate> CreateAsync(SmsTemplate smsTemplate, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var validationResult = _smsTemplateValidator.Validate(smsTemplate);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        return _smsTemplateRepository.CreateAsync(smsTemplate, saveChanges, cancellationToken);
    }
}