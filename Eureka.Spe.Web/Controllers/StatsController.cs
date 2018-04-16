using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;
using Eureka.Spe.Stats;

namespace Eureka.Spe.Web.Controllers
{
    [AbpMvcAuthorize]
    public class StatsController : Controller
    {
        private readonly IStatsAppService _statsAppService;

        public StatsController(IStatsAppService statsAppService)
        {
            _statsAppService = statsAppService;
        }

        // GET: Stats
        public ActionResult GetStatsForEntity(string entityName,int entityId)
        {
            ViewBag.EntityName = entityName;
            ViewBag.EntityId = entityId;


            return View();
        }
    }
}