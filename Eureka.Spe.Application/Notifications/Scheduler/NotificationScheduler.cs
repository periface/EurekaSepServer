using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Abp.BackgroundJobs;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Eureka.Spe.NewsFeed.Entities;
using Eureka.Spe.PhoneNotifications.Entities;
using Eureka.Spe.Students.Entities;

namespace Eureka.Spe.Notifications.Scheduler
{
    public class NotificationScheduler: BackgroundJob<SendNotificationArgs>, ITransientDependency
    {
        private readonly IRepository<Student> _students;
        private readonly IRepository<PhoneNotification> _phoneNotificationRepository;
        private readonly IRepository<Feed> _feedRepository;
        public NotificationScheduler(IRepository<Student> students, IRepository<PhoneNotification> phoneNotificationRepository, IRepository<Feed> feedRepository)
        {
            _students = students;
            _phoneNotificationRepository = phoneNotificationRepository;
            _feedRepository = feedRepository;
        }
        [UnitOfWork]
        public override void Execute(SendNotificationArgs args)
        {
            var notification = _phoneNotificationRepository
                .GetAllIncluding(a => a.SendNotificationsStatuses)
                .FirstOrDefault(a=>a.Id == args.NotificationId);
            if (notification != null && notification.SendNotificationsStatuses == null) notification.SendNotificationsStatuses = new List<SendNotificationsStatus>();
            var tokens = GetPhoneTokens(notification);
            if (notification == null) return;
            foreach (var token in tokens)
            {
                notification.SendNotificationsStatuses.Add(new SendNotificationsStatus()
                {
                    Token = token
                });
            }
        }

        private IEnumerable<string> GetPhoneTokens(PhoneNotification notification)
        {
            var result = new List<string>();
            switch (notification.AssignedTo)
            {
                case "feeds":
                    var feed = _feedRepository.GetAllIncluding(a => a.AcademicUnits).FirstOrDefault(a => a.Id == notification.AssignedToId);

                    if (feed == null) break;

                    //Obtiene todos los estudiantes de la unidad academica
                    var students = _students.GetAll()
                        .Include(a => a.Career).Include(a=>a.PhoneInfos).ToList();
                    var studentsEnum = students.Where(a => feed.AcademicUnits.Any(f => f.Id == a.Career.AcademicUnitId));
                    
                    foreach (var student in studentsEnum)
                    {
                        result.AddRange(student.PhoneInfos.Select(a => a.Token));
                    }
                    break;
                default:
                    break;
            }
            return result;
        }

    }
}
