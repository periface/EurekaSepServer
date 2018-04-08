using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eureka.Spe.Notifications.Dto
{
    public class StudentNotificationsResult
    {
       public List<SimpleNotificationResult> Notifications { get; set; } = new List<SimpleNotificationResult>();
    }

    public class SimpleNotificationResult
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public string Data { get; set; }
        public bool Readed { get; set; }
        public int Id { get; set; }
    }
}
