using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Eureka.Spe.CourseCategories.Dto;
using Eureka.Spe.Courses.Entities;
using Eureka.Spe.PaginableHelpers;

namespace Eureka.Spe.CourseCategories
{
    public interface ICourseCategoriesAppService :IApplicationService, IHavePaginatedResults<CourseCategory,CourseCategoryDto,BootstrapTableInput>
    {
        List<CourseCategoryDto> GetCourseList();
    }
}
