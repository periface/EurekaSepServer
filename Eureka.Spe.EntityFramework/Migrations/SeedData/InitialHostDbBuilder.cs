using Eureka.Spe.EntityFramework;
using EntityFramework.DynamicFilters;

namespace Eureka.Spe.Migrations.SeedData
{
    public class InitialHostDbBuilder
    {
        private readonly SpeDbContext _context;

        public InitialHostDbBuilder(SpeDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            _context.DisableAllFilters();

            new DefaultEditionsCreator(_context).Create();
            new DefaultLanguagesCreator(_context).Create();
            new HostRoleAndUserCreator(_context).Create();
            new DefaultSettingsCreator(_context).Create();
        }
    }
}
