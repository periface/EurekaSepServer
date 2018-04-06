using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Eureka.Spe.Stats.Dto;
using Eureka.Spe.Stats.Entities;

namespace Eureka.Spe.Stats
{
    public class StatsAppService : IStatsAppService

    {
        private readonly IRepository<MetricElement> _metricElementRepository;
        private readonly IRepository<ClickElement> _clickElementRepository;

        public StatsAppService(IRepository<MetricElement> metricElementRepository, IRepository<ClickElement> clickElementRepository)
        {
            _metricElementRepository = metricElementRepository;
            _clickElementRepository = clickElementRepository;
        }

        public async Task AddClick(ClickDto input)
        {
            var mapped = input.MapTo<ClickElement>();
            await _clickElementRepository.InsertAndGetIdAsync(mapped);
        }

        public async Task AddStat(MetricDto input)
        {
            var mapped = input.MapTo<MetricElement>();
            await _metricElementRepository.InsertAndGetIdAsync(mapped);
        }




    }
}
