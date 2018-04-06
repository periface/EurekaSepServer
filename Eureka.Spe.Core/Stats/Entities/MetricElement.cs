using Abp.Domain.Entities.Auditing;

namespace Eureka.Spe.Stats.Entities
{
    public class MetricElement : FullAuditedEntity
    {
        public string EntityName { get; set; }
        public int EntityId { get; set; }

        public int? StudentId { get; set; }

        public string CategoryEntityName { get; set; }
        public int CategoryEntityId { get; set; }

        public int Note { get; set; }

        public string Comment { get; set; }
    }
}
