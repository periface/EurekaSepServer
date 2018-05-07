using Abp.Domain.Entities.Auditing;

namespace Eureka.Spe.Resources.Entities
{
    public class EntityResource : FullAuditedEntity
    {
        public string ResourceType { get; set; }
        public string ResourceFile { get; set; }
        public string ResourceUri { get; set; }
        public string EntityName { get; set; }
        public string EntityId { get; set; }
    }
}
