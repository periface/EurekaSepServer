using Abp.MultiTenancy;
using Eureka.Spe.Authorization.Users;

namespace Eureka.Spe.MultiTenancy
{
    public class Tenant : AbpTenant<User>
    {
        public Tenant()
        {
            
        }

        public Tenant(string tenancyName, string name)
            : base(tenancyName, name)
        {
        }
    }
}