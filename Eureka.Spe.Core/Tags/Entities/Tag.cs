using Abp.Domain.Entities.Auditing;

namespace Eureka.Spe.Tags.Entities
{
    public class Tag : FullAuditedEntity
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        
    }
}
