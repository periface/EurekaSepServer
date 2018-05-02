using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Eureka.Spe.NewsFeed.Entities;

namespace Eureka.Spe.NewsFeed.Publishers.Dto
{
    [AutoMap(typeof(FeedPublisher))]
    public class PublisherDto : FullAuditedEntityDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Img { get; set; }
        public int TenantId { get; set; }
    }
}
