using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Eureka.Spe.NewsFeed.Entities;

namespace Eureka.Spe.Feeds.Dto
{
    [AutoMap(typeof(Feed))]
    public class FeedDto : EntityDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string Img { get; set; }
        public int TenantId { get; set; }
        public bool Notify { get; set; }
    }
}
