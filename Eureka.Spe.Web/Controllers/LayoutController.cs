using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Abp.Application.Navigation;
using Abp.Configuration;
using Abp.Configuration.Startup;
using Abp.Localization;
using Abp.Runtime.Session;
using Abp.Threading;
using Abp.Web.Mvc.Authorization;
using Eureka.Spe.AcademicUnits;
using Eureka.Spe.AcademicUnits.Dto;
using Eureka.Spe.Careers;
using Eureka.Spe.Configuration;
using Eureka.Spe.Configuration.Ui;
using Eureka.Spe.CourseCategories;
using Eureka.Spe.Notifications;
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
        private readonly IAcademicUnitAppService _academicUnitAppService;
        private readonly ICareerAppService _careerAppService;
        private readonly ICourseCategoriesAppService _courseCategoriesAppService;
        private readonly INotificationAppService _notificationAppService;
        public LayoutController(
            IUserNavigationManager userNavigationManager,
            ISessionAppService sessionAppService,
            IMultiTenancyConfig multiTenancyConfig,
            ILanguageManager languageManager, IPublisherAppService publisherAppService, IAcademicUnitAppService academicUnitAppService, ICareerAppService careerAppService, ICourseCategoriesAppService courseCategoriesAppService, INotificationAppService notificationAppService)
        {
            _userNavigationManager = userNavigationManager;
            _sessionAppService = sessionAppService;
            _multiTenancyConfig = multiTenancyConfig;
            _languageManager = languageManager;
            _publisherAppService = publisherAppService;
            _academicUnitAppService = academicUnitAppService;
            _careerAppService = careerAppService;
            _courseCategoriesAppService = courseCategoriesAppService;
            _notificationAppService = notificationAppService;
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
        [AbpMvcAuthorize]
        public ViewResult PublishersSelector(int? selected)
        {
            ViewBag.Selected = selected ?? 0;

            var publishers = _publisherAppService.GetPublishersSimpleList();

            return View(publishers);

        }
        [ChildActionOnly]
        [AbpMvcAuthorize]
        public ViewResult AcademicUnitsSelector(int? selected)
        {
            ViewBag.Selected = selected ?? 0;

            List<AcademicUnitDto> academicUnits = _academicUnitAppService.GetAcademicUnitSimpleList();

            return View(academicUnits);

        }
        [ChildActionOnly]
        [AbpMvcAuthorize]
        public ViewResult CareersSelector(int? selected)
        {
            ViewBag.Selected = selected ?? 0;

            var careers = _careerAppService.GetCareersList();

            return View(careers);

        }
        [ChildActionOnly]
        [AbpMvcAuthorize]
        public ViewResult CourseCategorySelector(int? selected)
        {
            ViewBag.Selected = selected ?? 0;

            var categories = _courseCategoriesAppService.GetCourseList();

            return View(categories);

        }
        [AbpMvcAuthorize]
        public ViewResult GetAcademicUnitsForEntity(string entityName, int id)
        {
            
            var academicUnits = _academicUnitAppService.GetAcademicUnitSimpleListForEntity(entityName,id);
            return View(new Models.NotificationRequest.LayoutController.ElementsForEntity()
            {
                EntityName = entityName,
                Id = id,
                AcademicUnitsSelected = academicUnits
            });
        }
        
    }
}