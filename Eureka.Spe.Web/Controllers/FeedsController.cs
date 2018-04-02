using Abp.Web.Mvc.Authorization;
using Eureka.Spe.AcademicUnits;
using Eureka.Spe.AcademicUnits.Dto;
using Eureka.Spe.Authorization;
using Eureka.Spe.Feeds;
using Eureka.Spe.Feeds.Dto;
using Eureka.Spe.Publishers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Eureka.Spe.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Feeds)]
    public class FeedsController : Controller
    {
        private readonly IFeedAppService _feedAppService;
        private readonly IPublisherAppService _publisherAppService;
        private readonly IAcademicUnitAppService _academicUnitAppService;
        public FeedsController(IFeedAppService feedAppService, IPublisherAppService publisherAppService, IAcademicUnitAppService academicUnitAppService)
        {
            _feedAppService = feedAppService;
            _publisherAppService = publisherAppService;
            _academicUnitAppService = academicUnitAppService;
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

        public async Task<ActionResult> Manage(int id)
        {
            var academicUnits = _academicUnitAppService.GetAcademicUnitSimpleList();
            var feed = await _feedAppService.Get(id);

            return View(new ManageFeedViewModel()
            {
                AcademicUnits = academicUnits,
                Feed = feed
            });
        }
        public class ManageFeedViewModel
        {
            public List<AcademicUnitDto> AcademicUnits { get; set; } = new List<AcademicUnitDto>();
            public FeedDto Feed { get; set; }
        }
    }
}