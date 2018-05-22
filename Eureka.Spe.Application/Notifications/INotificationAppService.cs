using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Eureka.Spe.Notifications.Dto;
using Eureka.Spe.PaginableHelpers;
using Eureka.Spe.PhoneNotifications.Entities;

namespace Eureka.Spe.Notifications
{
    public interface INotificationAppService :IApplicationService, IHavePaginatedResults<PhoneNotification, NotificationDto,NotificationPaginableInput>
    {
        List<NotificationDto> GetNotificationsSimpleListForEntity(string entityName, int id);
        [HttpGet]
        StudentNotificationsResult GetNotificationsForStudent(int id);
        [HttpGet]
        int GetNotificationNotReadedForStudent(int id);
        [HttpGet]
        Task SetAsReaded(int id);

        PagedResultDto<SimpleNotificationResult> GetNotificationsForStudentPaged(CustomStudentNotificationsRequest input);
    }
}
