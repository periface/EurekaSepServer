using System.Collections.Generic;
using Abp.Application.Services;
using Eureka.Spe.ContinuousEducation.CourseCategories.Dto;
using Eureka.Spe.Courses.Entities;
using Eureka.Spe.PaginableHelpers;

namespace Eureka.Spe.ContinuousEducation.CourseCategories
{
    public interface ICourseCategoriesAppService :IApplicationService, IHavePaginatedResults<CourseCategory,CourseCategoryDto,BootstrapTableInput>
    {
        List<CourseCategoryDto> GetCourseList();
    }
}
