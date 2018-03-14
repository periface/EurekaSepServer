using System.Threading.Tasks;
using Abp.Application.Services;
using Eureka.Spe.NewsFeed.Entities;
using Eureka.Spe.PaginableHelpers;
using Eureka.Spe.Publishers.Dto;

namespace Eureka.Spe.Publishers
{
    public interface IPublisherAppService : IApplicationService, IHavePaginatedResults<FeedPublisher, PublisherDto, BootstrapTableInput>
    {
        Task CreateOrUpdate(PublisherDto input);
        Task Delete(int id);
        PublisherDto Get(int id);
    }
}
