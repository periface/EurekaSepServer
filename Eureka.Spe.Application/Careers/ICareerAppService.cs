using System.Collections.Generic;
using Abp.Application.Services;
using Eureka.Spe.Careers.Dto;
using Eureka.Spe.PaginableHelpers;
using Eureka.Spe.Students.Entities;

namespace Eureka.Spe.Careers
{
    public interface ICareerAppService : IApplicationService, IHavePaginatedResults<Career, CareerDto,CustomCareerInput>
    {
        List<CareerDto> GetCareersSimpleList();
        CareersGroupedList GetCareersList();
        List<CareerDto> GetCareersSimpleListForAcUnit(int id);
    }
}
