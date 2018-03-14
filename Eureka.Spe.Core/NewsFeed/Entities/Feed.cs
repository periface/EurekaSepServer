using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Eureka.Spe.Contracts;

namespace Eureka.Spe.NewsFeed.Entities
{
    public class Feed : FullAuditedEntity, IMustHaveTenant, IHasPublishableInfo
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string Img { get; set; }
        public int TenantId { get; set; }

        public int PublisherId { get; set; }

        [ForeignKey("PublisherId")]
        public virtual FeedPublisher Publisher { get; set; }


    }
}
