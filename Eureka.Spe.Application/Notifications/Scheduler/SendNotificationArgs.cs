using System;

namespace Eureka.Spe.Notifications.Scheduler
{
    [Serializable]
    public class SendNotificationArgs
    {
        public int NotificationId { get; set; }
        public string Token { get; set; }
    }
}