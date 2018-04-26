using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Configuration;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Eureka.Spe.DailyMessages.Dto;
using Eureka.Spe.DailyMessages.Entities;
using Eureka.Spe.PaginableHelpers;

namespace Eureka.Spe.DailyMessages
{
    public class DialyMessageAppService : IDialyMessageAppService
    {
        private readonly IRepository<Message> _repository;
        private readonly SettingManager _settingManager;
        public DialyMessageAppService(IRepository<Message> repository, SettingManager settingManager)
        {
            _repository = repository;
            _settingManager = settingManager;
        }

        public IQueryable<Message> GetFilteredQuery(IQueryable<Message> all, BootstrapTableInput input)
        {
            all = all.WhereIf(!string.IsNullOrEmpty(input.search), a => a.Title.Contains(input.search));
            return all;
        }

        public IQueryable<Message> GetOrderedQuery(IQueryable<Message> all, BootstrapTableInput input)
        {
            switch (input.sort)
            {
                case "title":
                    return input.order == "desc" ? all.OrderByDescending(a => a.Title) : all.OrderBy(a => a.Title);
                default:
                    return input.order == "desc" ? all.OrderByDescending(a => a.CreationTime) : all.OrderBy(a => a.CreationTime);
            }
        }

        public PagedResultDto<MessageDto> GetAll(BootstrapTableInput input)
        {
            var all = _repository.GetAllIncluding();

            var filtered = GetFilteredQuery(all, input);
            var ordered = GetOrderedQuery(filtered, input);

            var paged = ordered.Skip(input.offset).Take(input.limit).ToList();

            return new PagedResultDto<MessageDto>(filtered.Count(), BuildModel(paged));
        }

        private IReadOnlyList<MessageDto> BuildModel(List<Message> paged)
        {
            return paged.Select(a => a.MapTo<MessageDto>()).ToList();
        }

        public async Task<int> CreateOrUpdate(MessageDto input)
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

                var mapped = input.MapTo<Message>();
                await _repository.InsertOrUpdateAndGetIdAsync(mapped);
                return mapped.Id;
            }
        }

        public async Task Delete(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<MessageDto> Get(int id)
        {
            var message = await _repository.GetAsync(id);
            return message.MapTo<MessageDto>();
        }

        public MessageDto GetLatestMessage()
        {
            var latest = _repository.GetAll().OrderByDescending(a=>a.CreationTime).FirstOrDefault();
            return latest.MapTo<MessageDto>();
        }

        public async Task<MessageConfigurationDto> GetConfigModel()
        {
            var name = await _settingManager.GetSettingValueAsync("RectorName");
            var img = await _settingManager.GetSettingValueAsync("RectorImg");

            return new MessageConfigurationDto()
            {
                MainImg = img,
                Name = name
            };
        }
    }
}
