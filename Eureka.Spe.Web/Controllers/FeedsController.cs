using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Abp.UI;
using Abp.Web.Mvc.Authorization;
using Eureka.Spe.Authorization;
using Eureka.Spe.Feeds;
using Eureka.Spe.Feeds.Dto;
using Eureka.Spe.Publishers;

namespace Eureka.Spe.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Feeds)]
    public class FeedsController : Controller
    {
        private readonly IFeedAppService _feedAppService;
        private readonly IPublisherAppService _publisherAppService;
        public FeedsController(IFeedAppService feedAppService, IPublisherAppService publisherAppService)
        {
            _feedAppService = feedAppService;
            _publisherAppService = publisherAppService;
        }

        // GET: 
        public ActionResult Index()
        {
            var publishers = _publisherAppService.GetPublishersSimpleList();
            ViewBag.HasPublishers = publishers.Any();
            return View();
        }

        public async Task<ActionResult> CreateOrEdit(int? id)
        {
            
            if (!id.HasValue) return View(new FeedDto());
            var feed = await _feedAppService.Get(id.Value);
            return View(feed);
        }
        
    }
}