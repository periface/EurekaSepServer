using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Eureka.Spe.MultiTenancy;

namespace Eureka.Spe.Sessions.Dto
{
    [AutoMapFrom(typeof(Tenant))]
    public class TenantLoginInfoDto : EntityDto
    {
        public string TenancyName { get; set; }

        public string Name { get; set; }
    }
}