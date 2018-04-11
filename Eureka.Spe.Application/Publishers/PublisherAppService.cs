using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Eureka.Spe.NewsFeed.Entities;
using Eureka.Spe.PaginableHelpers;
using Eureka.Spe.Publishers.Dto;

namespace Eureka.Spe.Publishers
{
    public class PublisherAppService : IPublisherAppService
    {
        private readonly IRepository<FeedPublisher> _repository;

        public PublisherAppService(IRepository<FeedPublisher> repository)
        {
            _repository = repository;
        }

        public PagedResultDto<PublisherDto> GetAll(BootstrapTableInput input)
        {
            var all = _repository.GetAllIncluding(a => a.Feeds);

            var filtered = GetFilteredQuery(all, input);

            var ordered = GetOrderedQuery(filtered, input);

            var paginated = ordered.Skip(input.offset).Take(input.limit).ToList();

            return new PagedResultDto<PublisherDto>(filtered.Count(), paginated.Select(a => a.MapTo<PublisherDto>()).ToList());
        }

        public IQueryable<FeedPublisher> GetOrderedQuery(IQueryable<FeedPublisher> all, BootstrapTableInput input)
        {
            switch (input.sort)
            {
                case "name":
                    return input.order == "desc" ? all.OrderByDescending(a => a.Name) : all.OrderBy(a => a.Name);
                default:
                    return input.order == "desc" ? all.OrderByDescending(a => a.CreationTime) : all.OrderBy(a => a.CreationTime);
            }
        }

        public IQueryable<FeedPublisher> GetFilteredQuery(IQueryable<FeedPublisher> query, BootstrapTableInput input)
        {
            query = query.WhereIf(!string.IsNullOrEmpty(input.search),
                a => a.Description.Contains(input.search) || a.Name.Contains(input.search));
            return query;
        }

        public async Task<int> CreateOrUpdate(PublisherDto input)
        {
            var mapped = input.MapTo<FeedPublisher>();
            await _repository.InsertOrUpdateAndGetIdAsync(mapped);
            return mapped.Id;
        }

        public async Task Delete(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task <PublisherDto> Get(int id)
        {
            var publisher = await _repository.GetAsync(id);
            return publisher.MapTo<PublisherDto>();
        }

        public List<PublisherDto> GetPublishersSimpleList()
        {
            var all = _repository.GetAllList();
            return all.Select(a => a.MapTo<PublisherDto>()).ToList();
        }
    }
}
