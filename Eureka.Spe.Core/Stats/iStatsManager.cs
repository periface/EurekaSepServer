using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Services;
using Eureka.Spe.Stats.Entities;

namespace Eureka.Spe.Stats
{
    public interface IStatsManager : IDomainService
    {
        IQueryable<MetricElement> GetMetricsForEntityType(string entityType);
        IQueryable<MetricElement> GetMetricsForOneEntitiesInType(string entityType, int entityId);
        IQueryable<ClickElement> GeClicksForEntityType(string entityType);
        IQueryable<ClickElement> GetClicksForOneEntitiesInType(string entityType, int entityId);
    }
}
