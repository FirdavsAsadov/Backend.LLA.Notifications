using AutoMapper;
using Notifications.Infrastructure.Application.Common.Notifications.Models;
using Notifications.Infrastructure.Domain.Entiteis;

namespace Notifications.Infrastructure.Infrastructure.Common.Mappers;

public class NotificationHistoryMappers : Profile
{
    public NotificationHistoryMappers()
    {
        CreateMap<EmailMessage, EmailHistory>()
            .ForMember(dest => dest.TempalateId, opt => opt.MapFrom(src => src.Template.Id))
            .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Body));

        CreateMap<SmsMessage, SmsHistory>()
            .ForMember(dest => dest.TempalateId, opt => opt.MapFrom(src => src.Template.Id))
            .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Message));
    }
}