﻿using System.Threading.Tasks;
using System.Web.Http;
using Abp.Application.Services;
using Eureka.Spe.Feeds.Dto;
using Eureka.Spe.NewsFeed.Entities;
using Eureka.Spe.PaginableHelpers;

namespace Eureka.Spe.Feeds
{
    public interface IFeedAppService : IApplicationService, IHavePaginatedResults<Feed, FeedDto, BootstrapTableInput>
    {
        Task Notify(int feedId);
        [HttpGet]
        Task<int> GetFeedDifCount(int input);
    }
}
