using Abp.Domain.Entities.Auditing;

namespace Eureka.Spe.Tags.Entities
{
    public class Tag : FullAuditedEntity
    {
        public string Name { get; set; }
        public bool PositiveBehavior { get; set; }
        public bool PositiveBehaviorValue { get; set; }

        public int StudentId { get; set; }
        public string EntityName { get; set; }
        public int EntityId { get; set; }
    }
}
