using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Eureka.Spe.AcademicUnits.Dto;
using Eureka.Spe.PaginableHelpers;
using Eureka.Spe.Students.Entities;

namespace Eureka.Spe.AcademicUnits
{
    public interface IAcademicUnitAppService : IApplicationService, IHavePaginatedResults<AcademicUnit,AcademicUnitDto,BootstrapTableInput>
    {
        List<AcademicUnitDto> GetAcademicUnitSimpleList();
        List<AcademicUnitSelectedDto> GetAcademicUnitSimpleListForEntity(string entityName, int id);
        Task AddAcademicUnitToEntity(AddToEntityInput input);
    }
}
