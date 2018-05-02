using System.Threading.Tasks;
using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;
using Eureka.Spe.Authorization;
using Eureka.Spe.ContinuousEducation.Courses;
using Eureka.Spe.ContinuousEducation.Courses.Dto;
using Eureka.Spe.Courses;

namespace Eureka.Spe.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Courses)]
    public class CoursesController : Controller
    {
        private readonly ICourseAppService _courseAppService;

        public CoursesController(ICourseAppService courseAppService)
        {
            _courseAppService = courseAppService;
        }

        // GET: Courses
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> CreateOrEdit(int? id)
        {
            if (!id.HasValue) return View(new CourseDto());
            var course = await _courseAppService.Get(id.Value);
            return View(course);
        }

        public async Task<ActionResult> Manage(int id)
        {
            var model = await _courseAppService.Get(id);
            return View(model);
        }
    }
}