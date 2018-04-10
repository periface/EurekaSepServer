using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Abp.Application.Services;
using Eureka.Spe.ScholarshipSections.Dto;

namespace Eureka.Spe.ScholarshipSections
{
    public interface IScholarshipSectionAppService : IApplicationService
    {
        Task CreateOrUpdate(ScholarshipSectionDto input);
        Task Delete(int id);
        [HttpGet]
        Task<ScholarshipSectionDto> Get(int id);
        [HttpGet]
        Task<List<ScholarshipSectionDto>> GetSections(int scholarshipId);
    }
}
