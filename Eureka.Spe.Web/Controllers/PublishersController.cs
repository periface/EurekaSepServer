using System.Web.Mvc;
using Eureka.Spe.Publishers;
using Eureka.Spe.Publishers.Dto;

namespace Eureka.Spe.Web.Controllers
{
    public class PublishersController : Controller
    {
        private IPublisherAppService _publisherAppService;

        public PublishersController(IPublisherAppService publisherAppService)
        {
            _publisherAppService = publisherAppService;
        }

        // GET: Publishers
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateOrEdit(int? id)
        {
            if (!id.HasValue) return View(new PublisherDto());
            var found = _publisherAppService.Get(id.Value);
            return View(found);
        }
    }
}