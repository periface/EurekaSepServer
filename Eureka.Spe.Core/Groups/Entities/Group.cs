using Abp.Domain.Entities.Auditing;

namespace Eureka.Spe.Groups.Entities
{
    public class Group : FullAuditedEntity
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}
