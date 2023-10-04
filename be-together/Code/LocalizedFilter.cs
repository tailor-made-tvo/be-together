using System.Threading;
using System.Web.Mvc;

namespace TMS.be_together.Code
{
    public class LocalizedFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            System.Globalization.CultureInfo culture = System.Globalization.CultureInfo.CreateSpecificCulture(LanguageHelper.GetLanguage());
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            base.OnActionExecuting(filterContext);
        }

    }
}
