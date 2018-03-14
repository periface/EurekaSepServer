using System.Collections.Generic;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Eureka.Spe.Courses.Entities
{
    public class CourseCategory : FullAuditedEntity, IMustHaveTenant
    {
        public string Name { get; set; }
        public string Image { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
        public int TenantId { get; set; }


    }
}
