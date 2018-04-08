using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Eureka.Spe.Contracts;

namespace Eureka.Spe.Students.Entities
{
    public class Student : FullAuditedEntity , IHasPersonInfo,IMustHaveTenant
    {
        public string Name { get; set; }
        public string Img { get; set; }
        public string Surname { get; set; }
        public string EnrollCode { get; set; }
        public int SchoolStudentId { get; set; }
        public int CareerId { get; set; }
        [ForeignKey("CareerId")]
        public virtual Career Career { get; set; }

        public virtual ICollection<PhoneInfo> PhoneInfos { get; set; }
        public int TenantId { get; set; }
        public string Password { get; set; }
    }
}
