using System.Threading.Tasks;
using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;
using Eureka.Spe.Feeds;
using Eureka.Spe.Feeds.Dto;

namespace Eureka.Spe.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : SpeControllerBase
    {
        private readonly IFeedAppService _feedAppService;

        public HomeController(IFeedAppService feedAppService)
        {
            _feedAppService = feedAppService;
        }

        public async Task<ActionResult> Index()
        {
            return View();
        }
	}
}