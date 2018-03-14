using System.Reflection;
using Abp.Modules;

namespace Eureka.Spe.Push
{
    public class EurekaPushModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
