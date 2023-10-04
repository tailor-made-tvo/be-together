using System.Web.Mvc;

namespace TMS.be_together.Code
{
    public class GlobalizationWebFormViewEngine : WebFormViewEngine
    {
        protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath)
        {
            return new GlobalizationWebFormView(controllerContext, viewPath, masterPath);
        }

        protected override IView CreatePartialView(ControllerContext controllerContext, string partialPath)
        {
            return new GlobalizationWebFormView(controllerContext, partialPath, null);
        }
    }
}