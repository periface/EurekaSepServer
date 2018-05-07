using System.Linq;
using Abp.Domain.Services;
using Eureka.Spe.Resources.Entities;

namespace Eureka.Spe.Resources
{
    public interface IResourceManager : IDomainService
    {
        IQueryable<EntityResource> GetResourcesForEntityType(string entityType);
        IQueryable<EntityResource> GetResourcesForOneEntitiesInType(string entityType, string entityId);
    }
}
