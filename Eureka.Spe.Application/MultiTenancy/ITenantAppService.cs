using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Eureka.Spe.MultiTenancy.Dto;

namespace Eureka.Spe.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}
