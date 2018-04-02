using System.Threading.Tasks;
using Abp.Application.Services;
using Eureka.Spe.Courses.Dto;
using Eureka.Spe.Courses.Entities;
using Eureka.Spe.PaginableHelpers;

namespace Eureka.Spe.Courses
{
    public interface ICourseAppService : IApplicationService, IHavePaginatedResults<Course,CourseDto,BootstrapTableInput>
    {
    }
}
