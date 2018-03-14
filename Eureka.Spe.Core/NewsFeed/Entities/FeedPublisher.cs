using System.Collections.Generic;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Eureka.Spe.Contracts;

namespace Eureka.Spe.NewsFeed.Entities
{
    public class FeedPublisher : FullAuditedEntity, IHasBasicInfo, IMustHaveTenant
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Img { get; set; }
        public int TenantId { get; set; }

        public virtual ICollection<Feed> Feeds { get; set; }
    }
}