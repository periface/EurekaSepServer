using System.Threading.Tasks;
using Eureka.Spe.CourseCategories.Dto;
using Eureka.Spe.Courses.Entities;
using Eureka.Spe.PaginableHelpers;

namespace Eureka.Spe.CourseCategories
{
    public interface ICourseCategoriesAppService : IHavePaginatedResults<CourseCategory,CourseCategoryDto,BootstrapTableInput>
    {
        Task CreateOrUpdate(CourseCategoryDto input);
        Task Delete(int id);
    }
}
