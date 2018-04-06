using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Threading;
using Abp.Threading.BackgroundWorkers;
using Abp.Threading.Timers;
using Eureka.Spe.PhoneNotifications.Entities;
using Eureka.Spe.Push.PushManager;
using Eureka.Spe.Push.PushManager.Inputs;

namespace Eureka.Spe.Notifications.Job
{
    public class NotificationBackgroundJob : PeriodicBackgroundWorkerBase, ISingletonDependency
    {
        private readonly IRepository<PhoneNotification> _phoneNotificationsRepository;
        private readonly IPushManager _pushManager;
        public NotificationBackgroundJob(AbpTimer timer, IRepository<PhoneNotification> phoneNotificationsRepository, IPushManager pushManager) : base(timer)
        {
            _phoneNotificationsRepository = phoneNotificationsRepository;
            _pushManager = pushManager;
            timer.Period = 5000;
        }
        [UnitOfWork]
        protected override void DoWork()
        {
            var notifications = _phoneNotificationsRepository.GetAllIncluding(a => a.SendNotificationsStatuses);

            var notificationsOnThisDay = notifications.Where(a =>a.NotifyDate.Month == DateTime.Now.Month 
            && a.NotifyDate.Year == DateTime.Now.Year 
            && a.NotifyDate.Day == DateTime.Now.Day 
            && a.NotifyDate.Hour == DateTime.Now.Hour 
            && a.NotifyDate.Minute == DateTime.Now.Minute).ToList();


            foreach (var phoneNotification in notificationsOnThisDay)
            {
                foreach (var phoneNotificationSendNotificationsStatus
                    in phoneNotification.SendNotificationsStatuses
                    .Where(a => a.ResultContent == null && !a.Sent && !a.SendTried))
                {
                    var message = new PushMessageInput()
                    {
                        Desc = phoneNotification.Message,
                        Title = phoneNotification.Title,
                        Players = new List<string>() { phoneNotificationSendNotificationsStatus.Token }
                    };
                    var sendResult = AsyncHelper.RunSync(() => _pushManager.SendMessage(message));

                    if (sendResult.Failure <= 0)
                    {
                        phoneNotificationSendNotificationsStatus.Sent = true;
                    }
                    phoneNotificationSendNotificationsStatus.SendTried = true;
                    phoneNotificationSendNotificationsStatus.ResultContent = $"Estado http: {sendResult.Code} - Resultado: {sendResult.Response.FirstOrDefault()}";
                }
            }

        }
    }
}
