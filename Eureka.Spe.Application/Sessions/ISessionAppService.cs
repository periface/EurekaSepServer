using System.Threading.Tasks;
using Abp.Application.Services;
using Eureka.Spe.Sessions.Dto;

namespace Eureka.Spe.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
