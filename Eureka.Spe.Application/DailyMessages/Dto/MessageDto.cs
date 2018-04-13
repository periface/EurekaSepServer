using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Eureka.Spe.DailyMessages.Entities;

namespace Eureka.Spe.DailyMessages.Dto
{
    [AutoMap(typeof(Message))]
    public class MessageDto : FullAuditedEntityDto
    {
        public bool IsActive { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string Img { get; set; }
        public int TenantId { get; set; }
    }
}
