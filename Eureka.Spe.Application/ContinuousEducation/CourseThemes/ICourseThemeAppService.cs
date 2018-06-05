using System.Runtime.Serialization.Configuration;
using System.Threading.Tasks;
using System.Web.Http;
using Abp.Application.Services;
using Eureka.Spe.ContinuousEducation.CourseThemes.Dto;

namespace Eureka.Spe.ContinuousEducation.CourseThemes
{
    public interface ICourseThemeAppService : IApplicationService
    {
        Task<CourseThemeDto> GetTheme(int id);
        [HttpGet]
        Task<CoursThemesResult> GetThemesForCourse(int courseId);
        Task<int> CreateOrEditTheme(CourseThemeDto input);
        Task Delete(int id);
    }
}
