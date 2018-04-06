using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Eureka.Spe.Stats.Entities;

namespace Eureka.Spe.Stats.Dto
{
    [AutoMap(typeof(ClickElement))]
    public class ClickDto : FullAuditedEntityDto
    {
        public string EntityName { get; set; }
        public int EntityId { get; set; }
        public int? StudentId { get; set; }
    }
}