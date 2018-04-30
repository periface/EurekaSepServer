using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Eureka.Spe.PhoneNotifications.Entities;
using Eureka.Spe.Stats.Dto;
using Eureka.Spe.Stats.Entities;

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

                return BasicResult(phoneNotifications, input);
            }
            return GetFilteredResult(phoneNotifications, input);
        }

        private List<NotificationsResult> GetFilteredResult(IQueryable<PhoneNotification> phoneNotifications, GetSingleElementMetricRequest input)
        {
            var result = phoneNotifications.Where(a => a.CreationTime > DateTime.Now.AddDays(-7)).ToList();

            return null;
        }
        private List<NotificationsResult> BasicResult(IQueryable<PhoneNotification> phoneNotifications, GetSingleElementMetricRequest input)
        {
            var result = new List<NotificationsResult>();
            foreach (var phoneNotification in phoneNotifications)
            {
                if (phoneNotification != null)
                {
                    var phoneStatuses = phoneNotification.SendNotificationsStatuses;
                    List<SendNotificationsStatus> phoneStatusesList;
                    if (input.EndDate.HasValue && input.StartDate.HasValue)
                    {
                        phoneStatusesList = phoneStatuses.Where(a => a.CreationTime.Year <= input.EndDate.Value.Year &&
                                                                     a.CreationTime.Year >= input.StartDate.Value.Year &&

                                                                     a.CreationTime.Month <= input.EndDate.Value.Month &&
                                                                     a.CreationTime.Month >= input.StartDate.Value.Month
                            )
                            .ToList();
                        if (input.ByDay)
                        {
                            phoneStatusesList = phoneStatusesList
                                .Where(a => a.CreationTime.Day <= input.EndDate.Value.Day &&
                                            a.CreationTime.Day >= input.StartDate.Value.Day)
                                .ToList();
                        }
                    }
                    else
                    {
                        phoneStatusesList = phoneStatuses.ToList();
                    }
                    if (input.ByDay)
                    {
                        var query = phoneStatusesList.GroupBy(a => new { a.CreationTime.Year, a.CreationTime.Month, a.CreationTime.Day })
                            .Select(i => new
                            {
                                i.Key.Year,
                                i.Key.Month,
                                i.Key.Day,
                                i
                            });

                        foreach (var element in query.ToList())
                        {
                            var metricsResult = new NotificationsResult
                            {
                                Date = new DateTime(element.Year, element.Month, element.Day).ToString("dd-MMMM-yyyy"),
                                SeenCount = element.i.Count(a => a.Readed),
                                UnseenCount = element.i.Count(a => !a.Readed)
                            };
                            result.Add(metricsResult);
                        }
                    }
                    else
                    {
                        var query = phoneStatusesList.GroupBy(a => new { a.CreationTime.Year, a.CreationTime.Month })
                            .Select(i => new
                            {
                                i.Key.Year,
                                i.Key.Month,
                                i
                            });
                        foreach (var element in query.ToList())
                        {
                            var metricsResult = new NotificationsResult
                            {
                                Date = new DateTime(element.Year, element.Month, 1).ToString("MMMM-yyyy"),
                                SeenCount = element.i.Count(a => a.Readed),
                                UnseenCount = element.i.Count(a => !a.Readed && a.Sent),
                            };
                            result.Add(metricsResult);
                        }
                    }
                }
            }

            return result;
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
            if (input.EndDate.HasValue && input.StartDate.HasValue)
            {
                metrics = metrics.Where(a => a.CreationTime.Year <= input.EndDate.Value.Year &&
                                             a.CreationTime.Year >= input.StartDate.Value.Year &&
                                             
                                             a.CreationTime.Month <= input.EndDate.Value.Month &&
                                             a.CreationTime.Month >= input.StartDate.Value.Month
                                             );

                if (input.ByDay)
                {
                    metrics = metrics.Where(a => a.CreationTime.Day <= input.EndDate.Value.Day &&
                                                 a.CreationTime.Day >= input.StartDate.Value.Day);
                }
            }
            
            if (input.ByDay)
            {
                var query = metrics.GroupBy(a => new { a.CreationTime.Year, a.CreationTime.Month, a.CreationTime.Day })
                    .Select(i => new
                    {
                        i.Key.Year,
                        i.Key.Month,
                        i.Key.Day,
                        i
                    });

                foreach (var element in query.ToList())
                {
                    var metricsResult = new MetricsResult
                    {
                        Date = new DateTime(element.Year, element.Month, element.Day).ToString("dd-MMMM-yyyy"),
                        ElementsDtos = element.i.Select(a => a.MapTo<MetricDto>()).ToList()
                    };
                    result.Add(metricsResult);
                }
                return result;
            }
            else
            {
                
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
                        Date = new DateTime(element.Year, element.Month, 1).ToString("MMMM-yyyy"),
                        ElementsDtos = element.i.Select(a => a.MapTo<MetricDto>()).ToList()
                    };
                    result.Add(metricsResult);
                }
                return result;
            }
        }
        public List<ClicksResult> GetClickForElement(GetSingleElementMetricRequest input)
        {
            var result = new List<ClicksResult>();
            var metrics = _statsManager.GetClicksForOneEntitiesInType(input.EntityType, input.EntityId);

            if (input.EndDate.HasValue && input.StartDate.HasValue)
            {
                metrics = metrics.Where(a => a.CreationTime.Year <= input.EndDate.Value.Year &&
                                             a.CreationTime.Year >= input.StartDate.Value.Year &&

                                             a.CreationTime.Month <= input.EndDate.Value.Month &&
                                             a.CreationTime.Month >= input.StartDate.Value.Month
                );

                if (input.ByDay)
                {
                    metrics = metrics.Where(a => a.CreationTime.Day <= input.EndDate.Value.Day &&
                                                 a.CreationTime.Day >= input.StartDate.Value.Day);
                }
            }

            if (input.ByDay)
            {

                var query = metrics.GroupBy(a => new {a.CreationTime.Year, a.CreationTime.Month, a.CreationTime.Day})
                    .Select(i => new
                    {
                        i.Key.Year,
                        i.Key.Month,
                        i.Key.Day,
                        i
                    });
                foreach (var element in query.ToList())
                {
                    var metricsResult = new ClicksResult
                    {
                        Date = new DateTime(element.Year, element.Month, element.Day).ToString("dd-MMMM-yyyy"),
                        ClickDtos = element.i.Select(a => a.MapTo<ClickDto>()).ToList()
                    };
                    result.Add(metricsResult);
                }
                return result;
            }
            else
            {
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
                        Date = new DateTime(element.Year, element.Month, 1).ToString("MMMM-yyyy"),
                        ClickDtos = element.i.Select(a => a.MapTo<ClickDto>()).ToList()
                    };
                    result.Add(metricsResult);
                }
                return result;
            }
        }
    }
}
