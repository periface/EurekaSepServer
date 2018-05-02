using System.Threading.Tasks;
using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;
using Eureka.Spe.Authorization;
using Eureka.Spe.Students;
using Eureka.Spe.StudentsInfo.Students;
using Eureka.Spe.StudentsInfo.Students.Dto;

namespace Eureka.Spe.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Students)]
    public class StudentsController : Controller
    {
        private readonly IStudentAppService _studentAppService;

        public StudentsController(IStudentAppService studentAppService)
        {
            _studentAppService = studentAppService;
        }

        // GET: Students
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> CreateOrEdit(int? id)
        {
            if (!id.HasValue) return View(new StudentDto());
            var student = await _studentAppService.Get(id.Value);
            return View(student);
        }

        public async Task<ActionResult> Manage(int id)
        {
            var student = await _studentAppService.Get(id);
            return View(student);
        }
    }
}