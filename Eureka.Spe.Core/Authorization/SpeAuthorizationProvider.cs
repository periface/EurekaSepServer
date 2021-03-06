﻿using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace Eureka.Spe.Authorization
{
    public class SpeAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            context.CreatePermission(PermissionNames.Pages_Users, L("Users"));
            context.CreatePermission(PermissionNames.Pages_Courses, L("Courses"));
            context.CreatePermission(PermissionNames.Pages_Feeds, L("Feeds"));
            context.CreatePermission(PermissionNames.Pages_Scholaships, L("Scholarships"));
            context.CreatePermission(PermissionNames.Pages_Students, L("Students"));
            context.CreatePermission(PermissionNames.Pages_Roles, L("Roles"));
            context.CreatePermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, SpeConsts.LocalizationSourceName);
        }
    }
}
