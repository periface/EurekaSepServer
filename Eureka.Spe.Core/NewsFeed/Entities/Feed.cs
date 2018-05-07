using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Eureka.Spe.Contracts;
using Eureka.Spe.Students.Entities;

namespace Eureka.Spe.NewsFeed.Entities
{
    public sealed class Feed : FullAuditedEntity, IMustHaveTenant, IHasPublishableInfo, IHasAcademicUnits, IShouldBeActivable
    {
        public Feed()
        {
            AcademicUnits = new HashSet<AcademicUnit>();
        }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string Img { get; set; }
        public int TenantId { get; set; }

        public int PublisherId { get; set; }

        [ForeignKey("PublisherId")]
        public FeedPublisher Publisher { get; set; }

        public ICollection<AcademicUnit> AcademicUnits { get; set; }
        public bool IsActive { get; set; }
    }
}
