using System.Threading.Tasks;
using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;
using Eureka.Spe.AcademicUnits;
using Eureka.Spe.AcademicUnits.Dto;
using Eureka.Spe.Authorization;

namespace Eureka.Spe.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Students)]
    public class AcademicUnitsController : SpeControllerBase
    {
        private readonly IAcademicUnitAppService _academicUnitAppService;

        public AcademicUnitsController(IAcademicUnitAppService academicUnitAppService)
        {
            _academicUnitAppService = academicUnitAppService;
        }

        // GET: AcademicUnits
        public ActionResult Index()
        {
            return View();
        }


        public async Task <ActionResult> CreateOrEdit(int? id)
        {
            if (!id.HasValue) return View(new AcademicUnitDto());
            var elm = await _academicUnitAppService.Get(id.Value);
            return View(elm);
        }

        public async Task<ActionResult> Manage(int id)
        {
            var model = await _academicUnitAppService.Get(id);
            return View(model);
        }
    }
}