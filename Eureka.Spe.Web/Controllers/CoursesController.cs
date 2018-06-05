using System.Threading.Tasks;
using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;
using Eureka.Spe.Authorization;
using Eureka.Spe.ContinuousEducation.Courses;
using Eureka.Spe.ContinuousEducation.Courses.Dto;
using Eureka.Spe.ContinuousEducation.CourseThemes;
using Eureka.Spe.ContinuousEducation.CourseThemes.Dto;

namespace Eureka.Spe.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Courses)]
    public class CoursesController : Controller
    {
        private readonly ICourseThemeAppService _courseThemeAppService;
        private readonly ICourseAppService _courseAppService;

        public CoursesController(ICourseThemeAppService courseThemeAppService, ICourseAppService courseAppService)
        {
            _courseThemeAppService = courseThemeAppService;
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

        public async Task<ActionResult> CreateOrEditCourseTheme(int courseId, int? id)
        {

            if (id.HasValue && id.Value != 0)
            {
                var theme = await _courseThemeAppService.GetTheme(id.Value);
                return View(theme);
            }
            var model = new CourseThemeDto()
            {
                CourseId = courseId
            };

            return View(model);
        }

        public async Task<ActionResult> Themes(int courseId)
        {
            var themes =  await _courseThemeAppService.GetThemesForCourse(courseId);
            return View(themes);
        }

    }
}