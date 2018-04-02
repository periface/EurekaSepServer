using System.Collections.Generic;
using Abp.Application.Services;
using Eureka.Spe.Notifications.Dto;
using Eureka.Spe.PaginableHelpers;
using Eureka.Spe.PhoneNotifications.Entities;

namespace Eureka.Spe.Notifications
{
    public interface INotificationAppService :IApplicationService, IHavePaginatedResults<PhoneNotification, NotificationDto,NotificationPaginableInput>
    {
        List<NotificationDto> GetNotificationsSimpleListForEntity(string entityName, int id);
    }
}
