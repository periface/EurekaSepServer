using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Eureka.Spe.Stats.Dto;
using Eureka.Spe.Stats.Entities;

namespace Eureka.Spe.Stats
{
    public partial class StatsAppService : IStatsAppService

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
        public List<MetricsResult> GetMetricsForElement(GetSingleElementMetricRequest input)
        {

            var result = new List<MetricsResult>();
            var metrics = _statsManager.GetMetricsForOneEntitiesInType(input.EntityType, input.EntityId);
            var query = metrics.GroupBy(a => new { a.CreationTime.Year, a.CreationTime.Month })
                .Select(i => new
                {
                    i.Key.Year,
                    i.Key.Month,
                    i
                });

            foreach (var element in query.ToList())
            {
                var metricsResult = new MetricsResult
                {
                    Date = new DateTime(element.Year, element.Month,1).ToString("MMMM yyyy"),
                    ElementsDtos = element.i.Select(a => a.MapTo<MetricDto>()).ToList()
                };
                result.Add(metricsResult);
            }
            return result;
        }
        public List<ClicksResult> GetClickForElement(GetSingleElementMetricRequest input)
        {
            var result = new List<ClicksResult>();
            var metrics = _statsManager.GetClicksForOneEntitiesInType(input.EntityType, input.EntityId);

            var query = metrics.GroupBy(a => new { a.CreationTime.Year, a.CreationTime.Month })
                .Select(i => new
                {
                    i.Key.Year,
                    i.Key.Month,
                    i
                });

            foreach (var element in query.ToList())
            {
                var metricsResult = new ClicksResult
                {
                    Date = new DateTime(element.Year, element.Month, 1).ToString("MMMM yyyy"),
                    ClickDtos = element.i.Select(a => a.MapTo<ClickDto>()).ToList()
                };
                result.Add(metricsResult);
            }
            return result;
        }
    }
}
