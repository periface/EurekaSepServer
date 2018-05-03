using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Eureka.Spe.PhoneNotifications.Entities;
using Eureka.Spe.Stats.Dto;
using Eureka.Spe.Stats.Entities;
using Eureka.Spe.Stats.Helpers;

namespace Eureka.Spe.Stats
{
    public class StatsAppService : IStatsAppService

    {
        private readonly IRepository<MetricElement> _metricElementRepository;
        private readonly IRepository<ClickElement> _clickElementRepository;
        private readonly IRepository<PhoneNotification> _phoneNotificationsRepository;
        private readonly IStatsManager _statsManager;
        public StatsAppService(IRepository<MetricElement> metricElementRepository, IRepository<ClickElement> clickElementRepository, IStatsManager statsManager, IRepository<PhoneNotification> phoneNotificationsRepository)
        {
            _metricElementRepository = metricElementRepository;
            _clickElementRepository = clickElementRepository;
            _statsManager = statsManager;
            _phoneNotificationsRepository = phoneNotificationsRepository;
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

        public List<NotificationsResult> GetNotificationsStatsForElement(GetSingleElementMetricRequest input)
        {
            
            var phoneNotifications = _phoneNotificationsRepository.GetAllIncluding(a => a.SendNotificationsStatuses)
                .Where(a => a.AssignedTo == input.EntityType && a.AssignedToId == input.EntityId);


            if (string.IsNullOrEmpty(input.Filter))
            {

                return StatsFilters.FilterNotificationStats(phoneNotifications, input);
            }
            if(input.Filter == "all") return StatsFilters.FilterNotificationStats(phoneNotifications, input);
            return GetFilteredResultForStats(phoneNotifications, input);
        }

        private List<NotificationsResult> GetFilteredResultForStats(IQueryable<PhoneNotification> phoneNotifications, GetSingleElementMetricRequest input)
        {
            var filteredDate = DateHelper.GetFilteredDate(input.Filter);
            var result = phoneNotifications.Where(a => a.CreationTime < filteredDate);
            return StatsFilters.FilterNotificationStats(result, input);
        }
        public bool CanSendFeedBack(GetSingleElementMetricRequest input)
        {
            var metrics = _statsManager.GetMetricsForOneEntitiesInType(input.EntityType, input.EntityId);
            return metrics.Any(a => a.StudentId == input.StudentId);
        }
        public List<MetricsResult> GetMetricsForElement(GetSingleElementMetricRequest input)
        {

            var metrics = _statsManager.GetMetricsForOneEntitiesInType(input.EntityType, input.EntityId);

            return StatsFilters.FilterMetricsResult(metrics, input);
        }
        public List<ClicksResult> GetClickForElement(GetSingleElementMetricRequest input)
        {
            var metrics = _statsManager.GetClicksForOneEntitiesInType(input.EntityType, input.EntityId);

            return StatsFilters.FilterClicksResult(metrics,input);
        }
    }
}
