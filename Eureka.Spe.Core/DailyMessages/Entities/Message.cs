using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Eureka.Spe.Contracts;

namespace Eureka.Spe.DailyMessages.Entities
{
    public class Message : FullAuditedEntity, IShouldBeActivable, IHasPublishableInfo, IMustHaveTenant
    {
        public bool IsActive { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string Img { get; set; }
        public int TenantId { get; set; }
    }


    public class MessageConfiguration
    {
        public string MainImg { get; set; }
        public string Name { get; set; }
    }
}
