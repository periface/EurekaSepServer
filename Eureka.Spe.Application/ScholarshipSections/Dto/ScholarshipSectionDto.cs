using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Eureka.Spe.Scholarships.Entities;

namespace Eureka.Spe.ScholarshipSections.Dto
{
    [AutoMap(typeof(ScholarshipSection))]
    public class ScholarshipSectionDto : FullAuditedEntityDto
    {
        public int ScholarshipId { get; set; }
        public bool IsPrincipal { get; set; }
        public int Order { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string Img { get; set; }
    }
}
