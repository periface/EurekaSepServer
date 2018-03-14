using Abp.Web.Mvc.Views;

namespace Eureka.Spe.Web.Views
{
    public abstract class SpeWebViewPageBase : SpeWebViewPageBase<dynamic>
    {

    }

    public abstract class SpeWebViewPageBase<TModel> : AbpWebViewPage<TModel>
    {
        protected SpeWebViewPageBase()
        {
            LocalizationSourceName = SpeConsts.LocalizationSourceName;
        }
    }
}