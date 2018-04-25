using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Abp.AutoMapper;
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

            var academicUnits = _academicUnitAppService.GetAcademicUnitSimpleList()
                                .Select(a =>
                {
                    var mapped = a.MapTo<CustomDtoAcademicUnitDto>();
                    if (a.Id == id) mapped.IsCurrentlySelected = true;
                    return mapped;
                }).ToList();

            return View(new ManageViewModel()
            {
                SelectedAcademicUnit = model,
                AvailableAcademicUnits = academicUnits
            });
        }

        public class ManageViewModel
        {
            public AcademicUnitDto SelectedAcademicUnit { get; set; }
            public List<CustomDtoAcademicUnitDto> AvailableAcademicUnits { get; set; }
        }
        [AutoMap(typeof(AcademicUnitDto))]
        public class CustomDtoAcademicUnitDto : AcademicUnitDto
        {
            public bool IsCurrentlySelected { get; set; }
        }
    }
}