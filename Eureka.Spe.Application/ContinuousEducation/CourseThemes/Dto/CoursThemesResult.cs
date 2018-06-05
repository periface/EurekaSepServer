using System.Collections.Generic;
using Eureka.Spe.ContinuousEducation.Courses.Dto;

namespace Eureka.Spe.ContinuousEducation.CourseThemes.Dto
{
    public class CoursThemesResult
    {
        public int CourseId { get; set; }
        public List<CourseThemeDto> CourseThemes { get; set; } = new List<CourseThemeDto>();
    }
}