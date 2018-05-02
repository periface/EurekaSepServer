using System.Collections.Generic;
using Abp.Application.Services;
using Eureka.Spe.PaginableHelpers;
using Eureka.Spe.Students.Entities;
using Eureka.Spe.StudentsInfo.Careers.Dto;

namespace Eureka.Spe.StudentsInfo.Careers
{
    public interface ICareerAppService : IApplicationService, IHavePaginatedResults<Career, CareerDto,CustomCareerInput>
    {
        List<CareerDto> GetCareersSimpleList();
        CareersGroupedList GetCareersList();
        List<CareerDto> GetCareersSimpleListForAcUnit(int id);
    }
}
