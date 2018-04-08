using System.Linq;
using Abp.Domain.Repositories;
using Eureka.Spe.Stats.Entities;

namespace Eureka.Spe.Stats
{
    public class StatsManager : IStatsManager
    {
        private readonly IRepository<MetricElement> _metricRepository;
        private readonly IRepository<ClickElement> _clickRepository;
        public StatsManager(IRepository<MetricElement> metricRepository, IRepository<ClickElement> clickRepository)
        {
            _metricRepository = metricRepository;
            _clickRepository = clickRepository;
        }

        public IQueryable<MetricElement> GetMetricsForEntityType(string entityType)
        {
            return _metricRepository.GetAll().Where(a => a.EntityName == entityType);
        }

        public IQueryable<MetricElement> GetMetricsForOneEntitiesInType(string entityType,int entityId)
        {
            return _metricRepository.GetAll().Where(a => a.EntityName == entityType && a.EntityId == entityId);
        }
        public IQueryable<ClickElement> GeClicksForEntityType(string entityType)
        {
            return _clickRepository.GetAll().Where(a => a.EntityName == entityType);
        }

        public IQueryable<ClickElement> GetClicksForOneEntitiesInType(string entityType, int entityId)
        {
            return _clickRepository.GetAll().Where(a => a.EntityName == entityType && a.EntityId == entityId);
        }
    }
}
