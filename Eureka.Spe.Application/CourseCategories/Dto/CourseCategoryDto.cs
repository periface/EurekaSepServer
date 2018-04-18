using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Eureka.Spe.Courses.Entities;

namespace Eureka.Spe.CourseCategories.Dto
{
    [AutoMap(typeof(CourseCategory))]
    public class CourseCategoryDto : FullAuditedEntityDto
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public int TenantId { get; set; }
    }
}
