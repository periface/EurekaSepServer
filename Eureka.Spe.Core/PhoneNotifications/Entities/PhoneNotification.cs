using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Eureka.Spe.Contracts;

namespace Eureka.Spe.PhoneNotifications.Entities
{
    /// <summary>
    /// Notificaciones que serán agendadas para el sistema, son generales-> deberían aplicar para todas
    /// las entidades creadas
    /// </summary>
    public class PhoneNotification : FullAuditedEntity, IShouldBeActivable
    {
        public bool IsActive { get; set; }

        public string Title { get; set; }
        public string Message { get; set; }
        public DateTime NotifyDate { get; set; }
        public string Data { get; set; }
        //Scholarship,Feed,Course, etc.
        public string AssignedTo { get; set; }
        public int AssignedToId { get; set; } 
        public virtual ICollection<SendNotificationsStatus> SendNotificationsStatuses { get; set; }
        
    }

    public class SendNotificationsStatus : FullAuditedEntity
    {
        public int PhoneNotificationId { get; set; }
        [ForeignKey("PhoneNotificationId")]
        public virtual PhoneNotification PhoneNotification { get; set; }
        public string ResultContent { get; set; }

        public string Token { get; set; }
        public bool Sent { get; set; }
        public bool SendTried { get; set; }
    }
}
