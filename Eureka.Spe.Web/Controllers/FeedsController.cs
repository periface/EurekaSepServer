using System.Web.Mvc;
using Eureka.Spe.Feeds;
using Eureka.Spe.Feeds.Dto;

namespace Eureka.Spe.Web.Controllers
{
    public class FeedsController : Controller
    {
        private readonly IFeedAppService _feedAppService;

        public FeedsController(IFeedAppService feedAppService)
        {
            _feedAppService = feedAppService;
        }

        // GET: 
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateOrEdit(int? id)
        {
            if (!id.HasValue) return View(new FeedDto());
            var feed = _feedAppService.Get(id.Value);
            return View(feed);
        }
    }
}