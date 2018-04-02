using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.BackgroundJobs;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Eureka.Spe.Notifications.Dto;
using Eureka.Spe.Notifications.Scheduler;
using Eureka.Spe.PhoneNotifications.Entities;

namespace Eureka.Spe.Notifications
{
    public class NotificationAppService : INotificationAppService
    {
        private readonly IRepository<PhoneNotification> _repository;
        private readonly IBackgroundJobManager _backgroundJobManager;
        public NotificationAppService(IRepository<PhoneNotification> repository, IBackgroundJobManager backgroundJobManager)
        {
            _repository = repository;
            _backgroundJobManager = backgroundJobManager;
        }

        public IQueryable<PhoneNotification> GetFilteredQuery(IQueryable<PhoneNotification> all, NotificationPaginableInput input)
        {
            all = all.WhereIf(!string.IsNullOrEmpty(input.search), a => a.Title.Contains(input.search));
            return all;
        }

        public IQueryable<PhoneNotification> GetOrderedQuery(IQueryable<PhoneNotification> all, NotificationPaginableInput input)
        {
            switch (input.sort)
            {
                case "title":
                    return input.order == "desc" ? all.OrderByDescending(a => a.Title) : all.OrderBy(a => a.Title);
                default:
                    return input.order == "desc" ? all.OrderByDescending(a => a.CreationTime) : all.OrderBy(a => a.CreationTime);
            }
        }

        public PagedResultDto<NotificationDto> GetAll(NotificationPaginableInput input)
        {
            var all = _repository.GetAllIncluding(a => a.SendNotificationsStatuses);

            var filtered = GetFilteredQuery(all, input);

            var ordered = GetOrderedQuery(filtered, input);

            var paged = ordered.Skip(input.offset).Take(input.limit).ToList();
            return new PagedResultDto<NotificationDto>(filtered.Count(), paged.Select(a=>a.MapTo<NotificationDto>()).ToList());
        }

        public async Task CreateOrUpdate(NotificationDto input)
        {
            var elm = input.MapTo<PhoneNotification>();
            await _repository.InsertOrUpdateAndGetIdAsync(elm);
            await _backgroundJobManager.EnqueueAsync<NotificationScheduler, SendNotificationArgs>(new SendNotificationArgs()
            {
                NotificationId = elm.Id,
            });
        }

        public async Task Delete(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<NotificationDto> Get(int id)
        {
            var notification = await _repository.GetAsync(id);
            return notification.MapTo<NotificationDto>();
        }

        public List<NotificationDto> GetNotificationsSimpleListForEntity(string entityName, int id)
        {
            var notifications = _repository.GetAllIncluding(a => a.SendNotificationsStatuses);

            switch (entityName)
            {
                case "feeds":
                    notifications = notifications.Where(a => a.AssignedToId == id);
                    return notifications.ToList().Select(a => a.MapTo<NotificationDto>()).ToList();
            }
            return new List<NotificationDto>();
        }
    }
}
