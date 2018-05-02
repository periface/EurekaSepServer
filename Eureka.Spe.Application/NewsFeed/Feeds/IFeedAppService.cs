using System.Threading.Tasks;
using System.Web.Http;
using Abp.Application.Services;
using Eureka.Spe.NewsFeed.Entities;
using Eureka.Spe.NewsFeed.Feeds.Dto;
using Eureka.Spe.PaginableHelpers;

namespace Eureka.Spe.NewsFeed.Feeds
{
    public interface IFeedAppService : IApplicationService, IHavePaginatedResults<Feed, FeedDto, BootstrapTableInput>
    {
        [HttpGet]
        Task<int> GetFeedDifCount(int input);
    }
}
