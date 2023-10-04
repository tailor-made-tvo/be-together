using System.Web.Mvc;

namespace TMS.be_together.Code
{
    public class GlobalizationRazorViewEngine : RazorViewEngine
    {
        protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath)
        {
            //return base.CreateView(controllerContext, viewPath, masterPath);
            return new GlobalizationRazorView(controllerContext, viewPath, masterPath, true, FileExtensions, ViewPageActivator);
        }

        protected override IView CreatePartialView(ControllerContext controllerContext, string partialPath)
        {
            //return base.CreatePartialView(controllerContext, partialPath);
            return new GlobalizationRazorView(controllerContext, partialPath, null, false, FileExtensions, ViewPageActivator);
        }
    }
}