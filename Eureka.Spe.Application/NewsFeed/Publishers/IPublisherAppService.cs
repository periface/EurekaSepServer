using System.Collections.Generic;
using Abp.Application.Services;
using Eureka.Spe.NewsFeed.Entities;
using Eureka.Spe.NewsFeed.Publishers.Dto;
using Eureka.Spe.PaginableHelpers;

namespace Eureka.Spe.NewsFeed.Publishers
{
    public interface IPublisherAppService : IApplicationService, IHavePaginatedResults<FeedPublisher, PublisherDto, BootstrapTableInput>
    {
        List<PublisherDto> GetPublishersSimpleList();
    }
}
