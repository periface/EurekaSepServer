using System.Threading.Tasks;
using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;
using Eureka.Spe.Authorization;
using Eureka.Spe.Publishers;
using Eureka.Spe.Publishers.Dto;

namespace Eureka.Spe.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Feeds)]
    public class PublishersController : Controller
    {
        private readonly IPublisherAppService _publisherAppService;

        public PublishersController(IPublisherAppService publisherAppService)
        {
            _publisherAppService = publisherAppService;
        }

        // GET: Publishers
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> CreateOrEdit(int? id)
        {
            if (!id.HasValue) return View(new PublisherDto());
            var found = await _publisherAppService.Get(id.Value);
            return View(found);
        }

        public ActionResult CreateFast()
        {
            return View(new PublisherDto()
            {
                Img = "/images/user.png"
            });
        }
    }
}