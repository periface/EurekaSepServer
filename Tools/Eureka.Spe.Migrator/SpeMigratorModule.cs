using System.Data.Entity;
using System.Reflection;
using Abp.Modules;
using Eureka.Spe.EntityFramework;

namespace Eureka.Spe.Migrator
{
    [DependsOn(typeof(SpeDataModule))]
    public class SpeMigratorModule : AbpModule
    {
        public override void PreInitialize()
        {
            Database.SetInitializer<SpeDbContext>(null);

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}