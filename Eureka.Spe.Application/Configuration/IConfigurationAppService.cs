using System.Threading.Tasks;
using Abp.Application.Services;
using Eureka.Spe.Configuration.Dto;

namespace Eureka.Spe.Configuration
{
    public interface IConfigurationAppService: IApplicationService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}