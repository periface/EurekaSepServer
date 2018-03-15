using System.Reflection;
using System.Web.Http;
using Abp.Application.Services;
using Abp.Configuration.Startup;
using Abp.Modules;
using Abp.WebApi;
using Eureka.Spe.PaginableHelpers;

namespace Eureka.Spe.Api
{
    [DependsOn(typeof(AbpWebApiModule), typeof(SpeApplicationModule))]
    public class SpeWebApiModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            Configuration.Modules.AbpWebApi().DynamicApiControllerBuilder
                .ForAll<IApplicationService>(typeof(SpeApplicationModule).Assembly, "app").ForMethods(builder =>
                {
                    if (builder.Method.IsDefined(typeof(ApiIgnoreAttribute)))
                    {
                        builder.DontCreate = true;
                    }
                })
                .Build();



            Configuration.Modules.AbpWebApi().HttpConfiguration.Filters.Add(new HostAuthenticationFilter("Bearer"));
        }
    }
}
