using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Eureka.Spe.NewsFeed.Entities;
using Eureka.Spe.NewsFeed.Feeds.Dto;
using Eureka.Spe.PaginableHelpers;
using Eureka.Spe.Stats;

namespace Eureka.Spe.NewsFeed.Feeds
{
    public class FeedAppService : IFeedAppService
    {
        private readonly IRepository<Feed> _repository;
        private readonly IStatsManager _statsManager;
        public FeedAppService(IRepository<Feed> repository, IStatsManager statsManager)
        {
            _repository = repository;
            _statsManager = statsManager;
        }

        public IQueryable<Feed> GetFilteredQuery(IQueryable<Feed> all, BootstrapTableInput input)
        {
            all = all.WhereIf(!string.IsNullOrEmpty(input.search), a => a.Title.Contains(input.search) || a.Publisher.Name.Contains(input.search) 
            );
            return input.active.HasValue ? all.Where(a => a.IsActive == input.active.Value) : all;
        }

        public IQueryable<Feed> GetOrderedQuery(IQueryable<Feed> all, BootstrapTableInput input)
        {
            switch (input.sort)
            {
                case "title":
                    return input.order == "desc" ? all.OrderByDescending(a => a.Title) : all.OrderBy(a => a.Title);
                case "publisherName":
                    return input.order == "desc" ? all.OrderByDescending(a => a.Publisher.Name) : all.OrderBy(a => a.Publisher.Name);
                default:
                    return input.order == "desc" ? all.OrderByDescending(a => a.CreationTime) : all.OrderBy(a => a.CreationTime);
            }
        }

        public PagedResultDto<FeedDto> GetAll(BootstrapTableInput input)
        {
            var all = _repository.GetAllIncluding(a => a.Publisher);

            var filtered = GetFilteredQuery(all, input);
            var ordered = GetOrderedQuery(filtered, input);

            var paged = ordered.Skip(input.offset).Take(input.limit).ToList();

            return new PagedResultDto<FeedDto>(filtered.Count(), BuildModel(paged));
        }

        private IReadOnlyList<FeedDto> BuildModel(List<Feed> paged)
        {
            return paged.Select(a =>
            {
                var mapped = a.MapTo<FeedDto>();
                mapped.PublisherName = a.Publisher?.Name;
                mapped.PublisherImg = a.Publisher?.Img;
                mapped.Clicks = _statsManager.GetClicksForOneEntitiesInType("feeds", a.Id).Count();
                return mapped;
            }).ToList();
        }

        public async Task<int> CreateOrUpdate(FeedDto input)
        {

            if (input.Id != 0)
            {
                var found = _repository.Get(input.Id);
                var mapped = input.MapTo(found);
                await _repository.InsertOrUpdateAndGetIdAsync(mapped);
                return mapped.Id;
            }
            else
            {

                var mapped = input.MapTo<Feed>();
                await _repository.InsertOrUpdateAndGetIdAsync(mapped);
                return mapped.Id;
            }
        }

        public async Task Delete(int id)
        {
            await _repository.DeleteAsync(id);
        }
        
        private FeedDto Map(Feed input)
        {
            var mapped = input.MapTo<FeedDto>();
            mapped.PublisherName = input.Publisher?.Name;
            mapped.PublisherImg = input.Publisher?.Img;
            mapped.Clicks = _statsManager.GetClicksForOneEntitiesInType("feeds", input.Id).Count();
            return mapped;
        }
        public async Task<FeedDto> Get(int idValue)
        {
            var feed = await Task.FromResult(_repository.GetAllIncluding(a=>a.Publisher).FirstOrDefault(a=>a.Id == idValue));
            return Map(feed);
        }

        public async Task<int> GetFeedDifCount(int input)
        {
            var count = await Task.FromResult(_repository.GetAll().Count());
            var diff = (count - input);
            return diff;
        }

        public async Task ToggleFeed(int id)
        {
            var feed = await _repository.GetAsync(id);
            feed.IsActive = !feed.IsActive;
        }
    }
}
