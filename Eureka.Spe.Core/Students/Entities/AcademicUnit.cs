using System.Collections.Generic;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Eureka.Spe.Contracts;

namespace Eureka.Spe.Students.Entities
{
    public class AcademicUnit : FullAuditedEntity, IHasBasicInfo, IMustHaveTenant
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Img { get; set; }
        public virtual ICollection<Career> Careers { get; set; }
        public int TenantId { get; set; }
        public string ShortName { get; set; }
    }
}