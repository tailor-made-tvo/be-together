using System.Threading;
using System.Web.Mvc;
using TMS.be_together.Code;

namespace TMS.be_together.Controllers
{
    [LocalizedFilter]
    public class LanguageController : Controller
    {

        public ActionResult Switch(string language)
        {
            System.Globalization.CultureInfo culture = System.Globalization.CultureInfo.CreateSpecificCulture(language);
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
            LanguageHelper.SetLanguage(language);

            if (this.ControllerContext.HttpContext.Request.UrlReferrer != null)
            {
                return Redirect(this.ControllerContext.HttpContext.Request.UrlReferrer.ToString());
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

    }
}
