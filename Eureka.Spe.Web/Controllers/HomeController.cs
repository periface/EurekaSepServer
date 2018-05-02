using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;
using Eureka.Spe.NewsFeed.Feeds;
using Eureka.Spe.Stats;

namespace Eureka.Spe.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : SpeControllerBase
    {
        private readonly IFeedAppService _feedAppService;
        private readonly IStatsAppService _statsAppService;
        public HomeController(IFeedAppService feedAppService, IStatsAppService statsAppService)
        {
            _feedAppService = feedAppService;
            _statsAppService = statsAppService;
        }

        public ActionResult Index()
        {
            return View();
        }


        public class HomeViewModel
        {
            public int SentNotificationsCount { get; set; }
            public int ReadedNotificationsCount { get; set; }
            public int TotalClicks { get; set; }
            public int UseFullInfoCount { get; set; }

        }
    }
}