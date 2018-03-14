using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Eureka.Spe.Feeds.Dto;
using Eureka.Spe.NewsFeed.Entities;
using Eureka.Spe.PaginableHelpers;

namespace Eureka.Spe.Feeds
{
    public interface IFeedAppService : IApplicationService, IHavePaginatedResults<Feed, FeedDto, BootstrapTableInput>
    {
        Task CreateOrUpdate(FeedDto input);
        Task Delete(int id);
        Task Notify(int feedId);
    }
}
