using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Eureka.Spe.Feeds;
using Eureka.Spe.Notifications;
using Eureka.Spe.Notifications.Dto;

namespace Eureka.Spe.Web.Controllers
{
    public class NotificationsController : Controller
    {
        private readonly INotificationAppService _notificationAppService;
        private readonly IFeedAppService _feedAppService;
        public NotificationsController(INotificationAppService notificationAppService, IFeedAppService feedAppService)
        {
            _notificationAppService = notificationAppService;
            _feedAppService = feedAppService;
        }

        // GET: Notifications
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Schedule(int? id, int? entityId, string entitype)
        {
            ViewBag.EntityType = entitype;
            if (id.HasValue)
            {
                var notification = await _notificationAppService.Get(id.Value);
                return View(notification);
            }
            if (!entityId.HasValue) return View(new NotificationDto(){ AssignedTo = entitype});
            switch (entitype)
            {
                case "feeds":
                    var feed = await _feedAppService.Get(entityId.Value);
                    return View(new NotificationDto()
                    {
                        AssignedTo = "feeds",
                        Data = "",
                        Message = feed.Description,
                        Title = feed.Title,
                        AssignedToId = entityId.Value
                    });
            }
            return View(new NotificationDto());
        }

    }
}