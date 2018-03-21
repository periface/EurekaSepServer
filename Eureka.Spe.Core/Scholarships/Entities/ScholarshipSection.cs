using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Eureka.Spe.Contracts;

namespace Eureka.Spe.Scholarships.Entities
{
    public class ScholarshipSection : FullAuditedEntity, IHasPublishableInfo
    {
        public int ScholarshipId { get; set; }

        [ForeignKey("ScholarshipId")]
        public virtual Scholarship Scholarship { get; set; }
        public bool IsPrincipal { get; set; }
        public int Order { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string Img { get; set; }
    }
}