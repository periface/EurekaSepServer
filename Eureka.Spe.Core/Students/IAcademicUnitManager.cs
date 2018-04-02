using System.Threading.Tasks;
using Abp.Domain.Services;

namespace Eureka.Spe.Students
{
    public interface IAcademicUnitManager : IDomainService
    {
        Task AddAcademicUnitsToEntity(string entityName,int entityId,int[] ids);
    }
}
