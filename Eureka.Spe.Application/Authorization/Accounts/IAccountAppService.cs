using System.Threading.Tasks;
using Abp.Application.Services;
using Eureka.Spe.Authorization.Accounts.Dto;

namespace Eureka.Spe.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
