using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Eureka.Spe.Contracts;

namespace Eureka.Spe.Courses.Entities
{
    public class Course : FullAuditedEntity,IHasPublishableInfo, IMustHaveTenant
    {
        public int CategoryId { get; set; }

        public CourseCategory CourseCategory { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public decimal? Price { get; set; }
        public string Img { get; set; }
        public int TenantId { get; set; }
    }
}