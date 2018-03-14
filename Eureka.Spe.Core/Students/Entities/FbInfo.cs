using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Eureka.Spe.Students.Entities
{
    public class FbInfo : FullAuditedEntity, IMustHaveTenant
    {
        public string Token { get; set; }
        public string Img { get; set; }
        public int StudentId { get; set; }
        public int TenantId { get; set; }
    }
}