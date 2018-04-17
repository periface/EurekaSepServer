using Abp.Application.Navigation;
using Abp.Localization;
using Eureka.Spe.Authorization;

namespace Eureka.Spe.Web
{
    /// <summary>
    /// This class defines menus for the application.
    /// It uses ABP's menu system.
    /// When you add menu items here, they are automatically appear in angular application.
    /// See Views/Layout/_TopMenu.cshtml file to know how to render menu.
    /// </summary>
    public class SpeNavigationProvider : NavigationProvider
    {
        public override void SetNavigation(INavigationProviderContext context)
        {
            context.Manager.MainMenu
                .AddItem(
                    new MenuItemDefinition(
                        PageNames.Home,
                        L("HomePage"),
                        url: "",
                        icon: "home",
                        requiresAuthentication: true
                    )
                )
                .AddItem(
                    new MenuItemDefinition(
                        PageNames.Tenants,
                        L("Tenants"),
                        url: "Tenants",
                        icon: "business",
                        requiredPermissionName: PermissionNames.Pages_Tenants
                    )
                )
                .AddItem(
                    new MenuItemDefinition(
                        PageNames.Users,
                        L("Users"),
                        url: "Users",
                        icon: "people",
                        requiredPermissionName: PermissionNames.Pages_Users
                    )
                )
                .AddItem(
                    new MenuItemDefinition(
                        PageNames.Roles,
                        L("Roles"),
                        url: "Roles",
                        icon: "local_offer",
                        requiredPermissionName: PermissionNames.Pages_Roles
                    )
                ).AddItem(
                    new MenuItemDefinition(
                            PageNames.Feeds,
                            L("Feeds"),
                            url: "Feeds",
                            icon: "local_offer",
                            requiredPermissionName: PermissionNames.Pages_Roles
                        )
                )
                .AddItem(
                    new MenuItemDefinition(
                            PageNames.Courses,
                            L("Courses"),
                            url: "",
                            icon: "local_offer"
                        )
                        .AddItem(
                            new MenuItemDefinition(
                                PageNames.CourseCategories,
                                L("Categories"),
                                url: "CourseCategories"
                            )
                        )
                        .AddItem(
                            new MenuItemDefinition(
                                PageNames.Courses,
                                L("CourseList"),
                                url: "Courses"
                            )
                        )
                )
                .AddItem(
                    new MenuItemDefinition(
                            PageNames.Scholarships,
                            L("Scholarships"),
                            url: "Scholarships",
                            icon: "local_offer"
                        )
                )
                .AddItem(
                    new MenuItemDefinition(
                        PageNames.Messages,
                        L("Messages"),
                        url: "Messages",
                        icon: "local_offer"
                    )
                )
                .AddItem(
                    new MenuItemDefinition(
                        PageNames.Students,
                        L("Students"),
                        icon: "local_offer"
                    ).AddItem(
                            new MenuItemDefinition(
                                PageNames.AcademicUnits,
                                L("AcademicUnits"),
                                url: "AcademicUnits"
                            )
                        )
                        .AddItem(
                            new MenuItemDefinition(
                                    PageNames.Careers,
                                    L("Careers"),
                                    url: "Careers"
                                ))
                        .AddItem(
                        new MenuItemDefinition(
                            PageNames.Students,
                            L("StudentsList"),
                            url: "Students"
                        )
                    )
                )
                .AddItem(
                    new MenuItemDefinition(
                        PageNames.About,
                        L("About"),
                        url: "About",
                        icon: "info"
                    )
                );
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, SpeConsts.LocalizationSourceName);
        }
    }
}
