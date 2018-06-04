using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Eureka.Spe.ContinuousEducation.Courses.Dto;
using Eureka.Spe.Courses.Entities;
using Eureka.Spe.PaginableHelpers;

namespace Eureka.Spe.ContinuousEducation.Courses
{
    public interface ICourseAppService : IApplicationService, IHavePaginatedResults<Course, CourseDto, BootstrapTableInput>
    {
        Task<CourseThemeDto> GetTheme(int id);
        Task<CoursThemesResult> GetThemes(int courseId);
        Task<int> CreateOrEditTheme(CourseThemeDto input);
    }
}
