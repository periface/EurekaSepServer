using Abp.Application.Services;
using Eureka.Spe.PaginableHelpers;
using Eureka.Spe.Scholarships.Dto;
using Eureka.Spe.Scholarships.Entities;

namespace Eureka.Spe.Scholarships
{
    public interface IScholarshipAppService : IApplicationService, IHavePaginatedResults<Scholarship,ScholarshipDto,BootstrapTableInput>
    {

    }
}
