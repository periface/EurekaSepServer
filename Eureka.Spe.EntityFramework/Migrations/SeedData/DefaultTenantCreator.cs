using System.Linq;
using Eureka.Spe.EntityFramework;
using Eureka.Spe.MultiTenancy;

namespace Eureka.Spe.Migrations.SeedData
{
    public class DefaultTenantCreator
    {
        private readonly SpeDbContext _context;

        public DefaultTenantCreator(SpeDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateUserAndRoles();
        }

        private void CreateUserAndRoles()
        {
            //Default tenant

            var defaultTenant = _context.Tenants.FirstOrDefault(t => t.TenancyName == Tenant.DefaultTenantName);
            if (defaultTenant == null)
            {
                _context.Tenants.Add(new Tenant {TenancyName = Tenant.DefaultTenantName, Name = Tenant.DefaultTenantName});
                _context.SaveChanges();
            }
        }
    }
}
