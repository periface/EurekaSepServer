using System;
using Abp.Web.Mvc.Authorization;
using Eureka.Spe.Authorization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Abp.UI;
using Eureka.Spe.NewsFeed.Feeds;
using Eureka.Spe.NewsFeed.Feeds.Dto;
using Eureka.Spe.NewsFeed.Publishers;
using Eureka.Spe.StudentsInfo.AcademicUnits;
using Eureka.Spe.StudentsInfo.AcademicUnits.Dto;

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
            var randomPublisher = _publisherAppService.GetPublishersSimpleList().FirstOrDefault();
            if (randomPublisher == null) throw new UserFriendlyException("No hay departamentos creados...");
            if (!id.HasValue)
            {
                var draft = await _feedAppService.CreateOrUpdate(new FeedDto()
                {
                    Title = $"Noticia {DateTime.Now.ToShortDateString()}",
                    Description = $"Noticia {DateTime.Now.ToShortDateString()}",
                    PublisherId = randomPublisher.Id
                });
                return RedirectToAction("CreateOrEdit", new { id = draft });
            };
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