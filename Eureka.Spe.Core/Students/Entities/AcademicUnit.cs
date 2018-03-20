using System.Collections.Generic;
using System.Collections.ObjectModel;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Eureka.Spe.Contracts;
using Eureka.Spe.NewsFeed.Entities;

namespace Eureka.Spe.Students.Entities
{
    public sealed class AcademicUnit : FullAuditedEntity, IHasBasicInfo, IMustHaveTenant, IHasFeeds
    {
        public AcademicUnit()
        {
            Feeds = new HashSet<Feed>();
        }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Img { get; set; }
        public ICollection<Career> Careers { get; set; }
        public int TenantId { get; set; }
        public string ShortName { get; set; }
        public ICollection<Feed> Feeds { get; set; }
    }
}