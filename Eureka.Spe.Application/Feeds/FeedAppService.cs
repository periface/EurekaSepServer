using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Eureka.Spe.Feeds.Dto;
using Eureka.Spe.NewsFeed.Entities;
using Eureka.Spe.PaginableHelpers;
using Eureka.Spe.Push.PushManager;
using Eureka.Spe.Push.PushManager.Inputs;

namespace Eureka.Spe.Feeds
{
    public class FeedAppService : IFeedAppService
    {
        private readonly IRepository<Feed> _repository;
        private readonly IPushManager _pushManager;
        public FeedAppService(IRepository<Feed> repository, IPushManager pushManager)
        {
            _repository = repository;
            _pushManager = pushManager;
        }

        public IQueryable<Feed> GetFilteredQuery(IQueryable<Feed> all, BootstrapTableInput input)
        {
            all = all.WhereIf(!string.IsNullOrEmpty(input.search), a => a.Title.Contains(input.search));
            return all;
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
                return mapped;
            }).ToList();
        }

        public async Task CreateOrUpdate(FeedDto input)
        {
            var mapped = input.MapTo<Feed>();
            await _repository.InsertOrUpdateAndGetIdAsync(mapped);
            if (input.Notify)
            {

                var result = await _pushManager.SendMessage(new PushMessageInput()
                {
                    Desc = mapped.Title,
                    Segments = new List<string>()
                    {
                        "All"
                    },
                    ElementId = mapped.Id.ToString(),
                    TypeOfElement = "FEED"
                });
            }
        }

        public async Task Delete(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task Notify(int feedId)
        {
            var feed = _repository.Get(feedId);

            await Task.Run(()=>_pushManager.SendMessage(new PushMessageInput()
            {
                Desc = feed.Title,
                ElementId = feed.Id.ToString(),
                TypeOfElement = "FEED",
                Segments = new List<string>() { "All" }
            }));
        }

        public async Task<FeedDto> Get(int idValue)
        {
            var feed = await _repository.GetAsync(idValue);
            return feed.MapTo<FeedDto>();
        }
    }
}
