using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using Eureka.Spe.Configuration.Dto;

namespace Eureka.Spe.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : SpeAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
