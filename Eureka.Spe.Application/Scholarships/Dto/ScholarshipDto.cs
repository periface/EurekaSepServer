using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Eureka.Spe.Scholarships.Entities;

namespace Eureka.Spe.Scholarships.Dto
{
    [AutoMap(typeof(Scholarship))]
    public class ScholarshipDto : EntityDto
    {
        public bool IsActive { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string Img { get; set; }
        public int? Sections { get; set; }
        public int TenantId { get; set; }


        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public bool HasDates => StartDate.HasValue && EndDate.HasValue;
    }
}
