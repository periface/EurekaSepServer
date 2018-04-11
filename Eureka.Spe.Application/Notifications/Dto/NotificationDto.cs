using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web.Script.Serialization;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Eureka.Spe.PhoneNotifications.Entities;

namespace Eureka.Spe.Notifications.Dto
{
    [AutoMap(typeof(PhoneNotification))]
    public class NotificationDto : EntityDto
    {
        public string Title { get; set; }

        public string Message { get; set; }

        public DateTime NotifyDate { get; set; } = DateTime.Now;
        public string Data { get; set; }
        //Scholarship,Feed,Course, etc.
        public string AssignedTo { get; set; }
        public int TenantId { get; set; }
        public int AssignedToId { get; set; }

        public void TurnToData()
        {
            var jsSerializer = new JavaScriptSerializer();
            Data = jsSerializer.Serialize(DataObj);
        }

        public dynamic DataObj { get; set; } = new ExpandoObject();
        public string Badge { get; set; }

        public int[] GetData
        {
            get
            {
                var readed = SimpleNotificationStatusInfos.Count(a => a.Seen && a.Sent);
                var notReaded = SimpleNotificationStatusInfos.Count(a => !a.Seen && a.Sent);
                var sent = SimpleNotificationStatusInfos.Count(a=>a.Sent && !a.Seen);
                var notSent = SimpleNotificationStatusInfos.Count(a => !a.Sent && !a.Seen);
                return new[]{ readed, notReaded, sent,notSent };
            }
        }

        public List<SimpleNotificationStatusInfo> SimpleNotificationStatusInfos { get; set; } = new List<SimpleNotificationStatusInfo>();
    }

    public class SimpleNotificationStatusInfo
    {
        public bool Seen { get; set; }
        public bool Sent { get; set; }
    }
}
