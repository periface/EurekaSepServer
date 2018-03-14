using Abp.AutoMapper;
using Eureka.Spe.Sessions.Dto;

namespace Eureka.Spe.Web.Models.Account
{
    [AutoMapFrom(typeof(GetCurrentLoginInformationsOutput))]
    public class TenantChangeViewModel
    {
        public TenantLoginInfoDto Tenant { get; set; }
    }
}