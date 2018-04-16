using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;
using Eureka.Spe.Authorization;
using Eureka.Spe.CourseCategories;
using Eureka.Spe.CourseCategories.Dto;

namespace Eureka.Spe.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Courses)]
    public class CourseCategoriesController : Controller
    {
        private readonly ICourseCategoriesAppService _courseCategoriesAppService;

        public CourseCategoriesController(ICourseCategoriesAppService courseCategoriesAppService)
        {
            _courseCategoriesAppService = courseCategoriesAppService;
        }

        // GET: CourseCategories
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> CreateOrEdit(int? id)
        {
            if (!id.HasValue) return View(new CourseCategoryDto());
            var category = await _courseCategoriesAppService.Get(id.Value);
            return View(category);
        }
    }
}