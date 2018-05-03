using System;
using System.Collections.Generic;
using System.Linq;
using Abp.AutoMapper;
using Eureka.Spe.PhoneNotifications.Entities;
using Eureka.Spe.Stats.Dto;
using Eureka.Spe.Stats.Entities;

namespace Eureka.Spe.Stats.Helpers
{
    public static class StatsFilters
    {
        public static List<NotificationsResult> FilterNotificationStats(IQueryable<PhoneNotification> phoneNotifications, GetSingleElementMetricRequest input)
        {
            var result = new List<NotificationsResult>();
            var list = phoneNotifications.ToList();
            foreach (var phoneNotification in list)
            {
                if (phoneNotification == null) continue;
                var phoneStatuses = phoneNotification.SendNotificationsStatuses;
                IEnumerable<SendNotificationsStatus> phoneStatusesList;
                if (input.EndDate.HasValue && input.StartDate.HasValue)
                {
                    phoneStatusesList = DateHelper<SendNotificationsStatus>.GetDateFilteredResult(phoneStatuses, input.StartDate.Value,
                        input.EndDate.Value, input.ByDay);
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
            return result;
        }

        public static List<MetricsResult> FilterMetricsResult(IQueryable<MetricElement> metrics,
            GetSingleElementMetricRequest input)
        {
            var result = new List<MetricsResult>();
            if (input.EndDate.HasValue && input.StartDate.HasValue)
            {
                metrics = DateHelper<MetricElement>.GetDateFilteredResult(metrics, input.StartDate.Value,
                    input.EndDate.Value, input.ByDay);
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

        public static List<ClicksResult> FilterClicksResult(IQueryable<ClickElement> metrics,
            GetSingleElementMetricRequest input)
        {
            var result = new List<ClicksResult>();
            if (input.EndDate.HasValue && input.StartDate.HasValue)
            {
                metrics = DateHelper<ClickElement>.GetDateFilteredResult(metrics, input.StartDate.Value,
                    input.EndDate.Value, input.ByDay);
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