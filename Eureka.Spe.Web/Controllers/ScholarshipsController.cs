using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Eureka.Spe.Scholarships;
using Eureka.Spe.Scholarships.Dto;

namespace Eureka.Spe.Web.Controllers
{
    
    public class ScholarshipsController : Controller
    {
        private readonly IScholarshipAppService _scholarshipAppService;

        public ScholarshipsController(IScholarshipAppService scholarshipAppService)
        {
            _scholarshipAppService = scholarshipAppService;
        }

        // GET: Scholarships
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> CreateOrEdit(int? id)
        {
            if (!id.HasValue) return View(new ScholarshipDto());
            var elm = await _scholarshipAppService.Get(id.Value);
            return View(elm);
        }

        public async Task<ActionResult> Manage(int id)
        {
            var sholarship = await _scholarshipAppService.Get(id);
            return View(sholarship);
        }


        public ActionResult Sections(int id)
        {
            return View();
        }

        public ActionResult Notifications(int id)
        {
            return View();
        }
    }
}