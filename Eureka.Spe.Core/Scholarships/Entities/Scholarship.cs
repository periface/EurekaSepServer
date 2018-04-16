using System;
using System.Collections.Generic;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Eureka.Spe.Contracts;

namespace Eureka.Spe.Scholarships.Entities
{
    public class Scholarship : FullAuditedEntity, IMustHaveTenant,IShouldBeActivable, IHasPublishableInfo
    {
        public bool IsActive { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string Img { get; set; }

        public virtual ICollection<ScholarshipSection> ScholarshipSections { get; set; }
        public int TenantId { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
