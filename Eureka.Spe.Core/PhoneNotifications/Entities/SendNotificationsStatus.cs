using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;

namespace Eureka.Spe.PhoneNotifications.Entities
{
    public class SendNotificationsStatus : FullAuditedEntity
    {
        public int PhoneNotificationId { get; set; }
        [ForeignKey("PhoneNotificationId")]
        public virtual PhoneNotification PhoneNotification { get; set; }
        public string ResultContent { get; set; }

        public string Token { get; set; }
        public bool Sent { get; set; }
        public bool SendTried { get; set; }
        public int? StudentId { get; set; }

        public bool Readed { get; set; }
    }
}