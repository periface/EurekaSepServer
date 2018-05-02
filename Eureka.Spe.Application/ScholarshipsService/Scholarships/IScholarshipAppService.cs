using Abp.Application.Services;
using Eureka.Spe.PaginableHelpers;
using Eureka.Spe.Scholarships.Entities;
using Eureka.Spe.ScholarshipsService.Scholarships.Dto;

namespace Eureka.Spe.ScholarshipsService.Scholarships
{
    public interface IScholarshipAppService : IApplicationService, IHavePaginatedResults<Scholarship,ScholarshipDto,BootstrapTableInput>
    {

    }
}
