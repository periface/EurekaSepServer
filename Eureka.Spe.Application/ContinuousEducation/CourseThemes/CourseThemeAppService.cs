using System.Linq;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Eureka.Spe.ContinuousEducation.CourseThemes.Dto;
using Eureka.Spe.Courses.Entities;

namespace Eureka.Spe.ContinuousEducation.CourseThemes
{
    public class CourseThemeAppService : ICourseThemeAppService
    {
        private readonly IRepository<CourseTheme> _repository;
        public CourseThemeAppService(IRepository<CourseTheme> repository)
        {
            _repository = repository;
        }
        public async Task<CourseThemeDto> GetTheme(int id)
        {
            var theme = await _repository.GetAsync(id);
            return theme.MapTo<CourseThemeDto>();
        }

        public async Task<CoursThemesResult> GetThemesForCourse(int courseId)
        {
            var themes = (await _repository
                .GetAllListAsync(a => a.CourseId == courseId))
                .OrderBy(a=>a.Order);
            var themesDto = themes.Select(a => a.MapTo<CourseThemeDto>())
                .ToList();
            return new CoursThemesResult()
            {
                CourseThemes = themesDto,
                CourseId = courseId
            };
        }

        public async Task<int> CreateOrEditTheme(CourseThemeDto input)
        {
            var elm = input.MapTo<CourseTheme>();
            await _repository.InsertOrUpdateAndGetIdAsync(elm);
            return elm.Id;
        }

        public async Task Delete(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
