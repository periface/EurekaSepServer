using System;
using System.Linq;
using Abp.Domain.Repositories;
using Eureka.Spe.Resources.Entities;

namespace Eureka.Spe.Resources
{
    public class ResourceManager : IResourceManager
    {
        private readonly IRepository<EntityResource> _resourceRepository;

        public ResourceManager(IRepository<EntityResource> resourceRepository)
        {
            _resourceRepository = resourceRepository;
        }

        public IQueryable<EntityResource> GetResourcesForEntityType(string entityType)
        {
            var query = _resourceRepository.GetAll().Where(a => a.EntityName == entityType);
            return query;
        }

        public IQueryable<EntityResource> GetResourcesForOneEntitiesInType(string entityType, string entityId)
        {
            var query = _resourceRepository.GetAll().Where(a => a.EntityName == entityType && a.EntityId == entityId);
            return query;
        }
    }
}
