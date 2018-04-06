using Abp.Domain.Entities.Auditing;

namespace Eureka.Spe.Stats.Entities
{
    public class ClickElement : FullAuditedEntity
    {
        public string EntityName { get; set; }
        public int EntityId { get; set; }
        public int? StudentId { get; set; }
    }
}