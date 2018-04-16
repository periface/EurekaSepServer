using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Abp.Web.Mvc.Authorization;
using Eureka.Spe.Authorization;
using Eureka.Spe.Courses;
using Eureka.Spe.DailyMessages;
using Eureka.Spe.Feeds;
using Eureka.Spe.Notifications;
using Eureka.Spe.Notifications.Dto;
using Eureka.Spe.Push.PushManager.Inputs;
using Eureka.Spe.Scholarships;

namespace Eureka.Spe.Web.Controllers
{
    [AbpMvcAuthorize]
    public class NotificationsController : Controller
    {
        private readonly INotificationAppService _notificationAppService;
        private readonly IFeedAppService _feedAppService;
        private readonly ICourseAppService _courseAppService;
        private readonly IScholarshipAppService _scoralshipAppService;
        private readonly IDialyMessageAppService _dialyMessageAppService;
        public NotificationsController(INotificationAppService notificationAppService, IFeedAppService feedAppService, ICourseAppService courseAppService, IScholarshipAppService scoralshipAppService, IDialyMessageAppService dialyMessageAppService)
        {
            _notificationAppService = notificationAppService;
            _feedAppService = feedAppService;
            _courseAppService = courseAppService;
            _scoralshipAppService = scoralshipAppService;
            _dialyMessageAppService = dialyMessageAppService;
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
            NotificationDto model;
            switch (entitype)
            {
                case "feeds":
                    var feed = await _feedAppService.Get(entityId.Value);
                    model = new NotificationDto()
                    {
                        AssignedTo = "feeds",
                        DataObj = new DataMessageRequest("feeds", entityId.Value),
                        Message = feed.Description,
                        Title = feed.Title,
                        AssignedToId = entityId.Value
                    };
                    model.TurnToData();
                    return View(model);
                case "courses":
                    var course = await _courseAppService.Get(entityId.Value);
                    model = new NotificationDto()
                    {
                        AssignedTo = "courses",
                        DataObj = new DataMessageRequest("courses", entityId.Value),
                        Message = course.Description,
                        Title = course.Title,
                        AssignedToId = entityId.Value
                    };
                    model.TurnToData();
                    return View(model);
                case "scholarships":
                    var scholarship = await _scoralshipAppService.Get(entityId.Value);
                    model = new NotificationDto()
                    {
                        AssignedTo = "scholarships",
                        DataObj = new DataMessageRequest("scholarships", entityId.Value),
                        Message = scholarship.Description,
                        Title = scholarship.Title,
                        AssignedToId = entityId.Value
                    };
                    model.TurnToData();
                    return View(model);
                case "messages":
                    var message = await _dialyMessageAppService.Get(entityId.Value);
                    model = new NotificationDto()
                    {
                        AssignedTo = "messages",
                        DataObj = new DataMessageRequest("messages",entityId.Value),
                        Message = message.Description,
                        Title = message.Title,
                        AssignedToId = entityId.Value
                    };
                    model.TurnToData();
                    return View(model);
            }
            return View(new NotificationDto());
        }

        
        public ViewResult GetNotificationsForEntity(string entityName, int id)
        {
            var notifications = _notificationAppService.GetNotificationsSimpleListForEntity(entityName, id);
            return View(new Models.NotificationRequest.LayoutController.ElementsForEntity()
            {
                EntityName = entityName,
                Id = id,
                Notifications = notifications
            });
        }

        public ViewResult GetStatusStats(int id)
        {
            return View();
        }
    }
}