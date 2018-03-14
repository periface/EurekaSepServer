using System.Web.Mvc;

namespace Eureka.Spe.Web.Controllers
{
    public class AboutController : SpeControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}