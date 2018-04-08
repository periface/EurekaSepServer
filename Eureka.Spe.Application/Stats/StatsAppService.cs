using System.Collections.Generic;
using System.Linq;
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
        private readonly IStatsManager _statsManager;
        public StatsAppService(IRepository<MetricElement> metricElementRepository, IRepository<ClickElement> clickElementRepository, IStatsManager statsManager)
        {
            _metricElementRepository = metricElementRepository;
            _clickElementRepository = clickElementRepository;
            _statsManager = statsManager;
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

        public bool CanSendFeedBack(GetSingleElementMetricRequest input)
        {
            var metrics = _statsManager.GetMetricsForOneEntitiesInType(input.EntityType, input.EntityId);
            return metrics.Any(a => a.StudentId == input.StudentId);
        }
        public List<MetricDto> GetMetricsForElement(GetSingleElementMetricRequest input)
        {
            var metrics = _statsManager.GetMetricsForOneEntitiesInType(input.EntityType, input.EntityId);
            return metrics.ToList().Select(a => a.MapTo<MetricDto>()).ToList();
        }

        public List<ClickDto> GetClickForElement(GetSingleElementMetricRequest input)
        {
            throw new System.NotImplementedException();
        }
    }
}
