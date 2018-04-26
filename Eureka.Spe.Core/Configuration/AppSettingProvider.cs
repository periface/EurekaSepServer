using System.Collections.Generic;
using Abp.Configuration;

namespace Eureka.Spe.Configuration
{
    public class AppSettingProvider : SettingProvider
    {
        public override IEnumerable<SettingDefinition> GetSettingDefinitions(SettingDefinitionProviderContext context)
        {
            return new[]
            {
                new SettingDefinition(AppSettingNames.UiTheme, "red", scopes: SettingScopes.Application | SettingScopes.Tenant | SettingScopes.User, isVisibleToClients: true),
                new SettingDefinition(AppSettingNames.RectorName, "John Doe", scopes: SettingScopes.Application, isVisibleToClients: true),
                new SettingDefinition(AppSettingNames.RectorMainImg, "https://diviestate.b3multimedia.ie/wp-content/uploads/2017/12/agent-04-400x400.jpg", scopes: SettingScopes.Application, isVisibleToClients: true),
            };
        }
    }
}