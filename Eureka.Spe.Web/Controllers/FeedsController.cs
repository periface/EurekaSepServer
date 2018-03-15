using System.Threading.Tasks;
using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;
using Eureka.Spe.Authorization;
using Eureka.Spe.Feeds;
using Eureka.Spe.Feeds.Dto;

namespace Eureka.Spe.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Feeds)]
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

        public async Task<ActionResult> CreateOrEdit(int? id)
        {
            if (!id.HasValue) return View(new FeedDto());
            var feed = await _feedAppService.Get(id.Value);
            return View(feed);
        }
    }
}