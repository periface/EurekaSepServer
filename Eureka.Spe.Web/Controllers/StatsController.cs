using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eureka.Spe.Stats;
using Eureka.Spe.Stats.Dto;

namespace Eureka.Spe.Web.Controllers
{
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