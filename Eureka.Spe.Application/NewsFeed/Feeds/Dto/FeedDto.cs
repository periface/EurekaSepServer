﻿using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Eureka.Spe.Contracts;
using Eureka.Spe.NewsFeed.Entities;

namespace Eureka.Spe.NewsFeed.Feeds.Dto
{
    [AutoMap(typeof(Feed))]
    public class FeedDto : FullAuditedEntityDto, IShouldBeActivable, IHasDesignOptions
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string Img { get; set; }
        public int TenantId { get; set; }
        public int PublisherId { get; set; }
        public string PublisherName { get; set; }
        public string PublisherImg { get; set; }
        public int Clicks { get; set; }
        public bool IsActive { get; set; }
        public string TitlePosition { get; set; }
        public string TitleColor { get; set; }
        public string FontSize { get; set; }
        public string FontWeight { get; set; }
        public bool DisplayTitleInList { get; set; }
        public bool DisplayTitleInDetails { get; set; }
    }

}
