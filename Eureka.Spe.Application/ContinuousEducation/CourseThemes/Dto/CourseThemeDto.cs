using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Eureka.Spe.Courses.Entities;

namespace Eureka.Spe.ContinuousEducation.CourseThemes.Dto
{
    [AutoMap(typeof(CourseTheme))]
    public class CourseThemeDto : FullAuditedEntityDto
    {
        public int CourseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string Img { get; set; }
        public int Order { get; set; }
    }
}