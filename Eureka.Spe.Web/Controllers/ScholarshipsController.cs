using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;
using Eureka.Spe.Authorization;
using Eureka.Spe.Scholarships;
using Eureka.Spe.Scholarships.Dto;
using Eureka.Spe.ScholarshipSections;
using Eureka.Spe.ScholarshipSections.Dto;

namespace Eureka.Spe.Web.Controllers
{
    [AbpMvcAuthorize]
    public class ScholarshipsController : Controller
    {
        private readonly IScholarshipAppService _scholarshipAppService;
        private readonly IScholarshipSectionAppService _scholarshipSectionAppService;
        public ScholarshipsController(IScholarshipAppService scholarshipAppService, IScholarshipSectionAppService scholarshipSectionAppService)
        {
            _scholarshipAppService = scholarshipAppService;
            _scholarshipSectionAppService = scholarshipSectionAppService;
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


        public async Task<ActionResult> Sections(int id)
        {
            ViewBag.ScholarshipId = id;
            var sections = await _scholarshipSectionAppService.GetSections(id);
            return View(sections);
        }

        public async Task<ActionResult> CreateOrEditSection(int? id,int scholarshipId)
        {
            if (!id.HasValue) return View(new ScholarshipSectionDto(){ScholarshipId = scholarshipId});
            var elm = await _scholarshipSectionAppService.Get(id.Value);
            return View(elm);
        }
    }
}