using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Eureka.Spe.Students.Entities
{
    public class PhoneInfo : FullAuditedEntity, IMustHaveTenant
    {
        public string Token { get; set; }
        public int StudentId { get; set; }
        [ForeignKey("StudentId")]
        public Student Student { get; set; }
        public string Cordova { get; set; }
        public string Model { get; set; }
        public string Platform { get; set; }
        public string Uuid { get; set; }
        public string Version { get; set; }
        public string Manufacturer { get; set; }
        public bool IsVirtual { get; set; }
        public string Serial { get; set; }
        public int TenantId { get; set; }
    }
}