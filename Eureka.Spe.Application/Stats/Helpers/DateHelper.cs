using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Domain.Entities.Auditing;

namespace Eureka.Spe.Stats.Helpers
{
    
    public static class DateHelper<T> 
        where T : FullAuditedEntity
    {
        public static IQueryable<T> GetDateFilteredResult(
            IQueryable<T> metrics,
            DateTime startDate,
            DateTime endDate,
            bool byDay)
        {
            metrics = metrics.Where(a => a.CreationTime.Year <= endDate.Year &&
                                         a.CreationTime.Year >= startDate.Year &&

                                         a.CreationTime.Month <= endDate.Month &&
                                         a.CreationTime.Month >= startDate.Month
            );

            if (byDay)
            {
                metrics = metrics.Where(a => a.CreationTime.Day <= endDate.Day &&
                                             a.CreationTime.Day >= startDate.Day);
            }
            return metrics;
        }
        public static IEnumerable<T> GetDateFilteredResult(
            IEnumerable<T> metrics,
            DateTime startDate,
            DateTime endDate,
            bool byDay)
        {
            metrics = metrics.Where(a => a.CreationTime.Year <= endDate.Year &&
                                         a.CreationTime.Year >= startDate.Year &&

                                         a.CreationTime.Month <= endDate.Month &&
                                         a.CreationTime.Month >= startDate.Month
            );

            if (byDay)
            {
                metrics = metrics.Where(a => a.CreationTime.Day <= endDate.Day &&
                                             a.CreationTime.Day >= startDate.Day);
            }
            return metrics;
        }
        
    }
    public static class DateHelper
    {
        public static DateTime GetFilteredDate(string filter)
        {
            var minusDays = DateTime.Now;
            switch (filter)
            {
                case "sevdays":
                    minusDays = minusDays.AddDays(-6);
                    break;
                case "thirdays":
                    minusDays = minusDays.AddDays(-29);
                    break;
                case "year":
                    minusDays = minusDays.AddDays(-364);
                    break;
            }
            return minusDays;
        }

    }
}
