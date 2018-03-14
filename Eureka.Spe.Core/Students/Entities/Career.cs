using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Eureka.Spe.Contracts;

namespace Eureka.Spe.Students.Entities
{
    public class Career : FullAuditedEntity, IHasBasicInfo, IMustHaveTenant
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Img { get; set; }
        public string Tags { get; set; }
        public int AcademicUnitId { get; set; }
        [ForeignKey("AcademicUnitId")]
        public virtual AcademicUnit AcademicUnit { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        public int TenantId { get; set; }
    }
}