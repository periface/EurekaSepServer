using System.Linq;
using System.Web.Mvc;
using Abp.Application.Navigation;
using Abp.Configuration;
using Abp.Configuration.Startup;
using Abp.Localization;
using Abp.Runtime.Session;
using Abp.Threading;
using Eureka.Spe.Configuration;
using Eureka.Spe.Configuration.Ui;
using Eureka.Spe.Publishers;
using Eureka.Spe.Sessions;
using Eureka.Spe.Web.Models;
using Eureka.Spe.Web.Models.Layout;

namespace Eureka.Spe.Web.Controllers
{
    public class LayoutController : SpeControllerBase
    {
        private readonly IUserNavigationManager _userNavigationManager;
        private readonly ISessionAppService _sessionAppService;
        private readonly IMultiTenancyConfig _multiTenancyConfig;
        private readonly ILanguageManager _languageManager;
        private readonly IPublisherAppService _publisherAppService;

        public LayoutController(
            IUserNavigationManager userNavigationManager,
            ISessionAppService sessionAppService,
            IMultiTenancyConfig multiTenancyConfig,
            ILanguageManager languageManager, IPublisherAppService publisherAppService)
        {
            _userNavigationManager = userNavigationManager;
            _sessionAppService = sessionAppService;
            _multiTenancyConfig = multiTenancyConfig;
            _languageManager = languageManager;
            _publisherAppService = publisherAppService;
        }

        [ChildActionOnly]
        public PartialViewResult SideBarNav(string activeMenu = "")
        {
            var model = new SideBarNavViewModel
            {
                MainMenu = AsyncHelper.RunSync(() => _userNavigationManager.GetMenuAsync("MainMenu", AbpSession.ToUserIdentifier())),
                ActiveMenuItemName = activeMenu
            };

            return PartialView("_SideBarNav", model);
        }

        [ChildActionOnly]
        public PartialViewResult SideBarUserArea()
        {
            var model = new SideBarUserAreaViewModel
            {
                LoginInformations = AsyncHelper.RunSync(() => _sessionAppService.GetCurrentLoginInformations()),
                IsMultiTenancyEnabled = _multiTenancyConfig.IsEnabled,
            };

            return PartialView("_SideBarUserArea", model);
        }

        [ChildActionOnly]
        public PartialViewResult LanguageSelection()
        {
            var model = new LanguageSelectionViewModel
            {
                CurrentLanguage = _languageManager.CurrentLanguage,
                Languages = _languageManager.GetLanguages()
            };

            return PartialView("_LanguageSelection", model);
        }

        [ChildActionOnly]
        public PartialViewResult RightSideBar()
        {
            var themeName = SettingManager.GetSettingValue(AppSettingNames.UiTheme);

            var viewModel = new RightSideBarViewModel
            {
                CurrentTheme = UiThemes.All.FirstOrDefault(t => t.CssClass == themeName)
            };

            return PartialView("_RightSideBar", viewModel);
        }

        [ChildActionOnly]
        public ViewResult PublishersSelector(int? selected)
        {
            ViewBag.Selected = selected ?? 0;

            var publishers = _publisherAppService.GetPublishersSimpleList();

            return View(publishers);

        }

    }
}