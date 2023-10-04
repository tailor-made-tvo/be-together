using System.IO;
using System.Web.Mvc;

namespace TMS.be_together.Code
{
    public class GlobalizationWebFormView : WebFormView
    {
        public GlobalizationWebFormView(ControllerContext controllerContext, string viewPath)
            : base(controllerContext, viewPath)
        {
        }

        public GlobalizationWebFormView(ControllerContext controllerContext, string viewPath, string masterPath)
            : base(controllerContext, viewPath, masterPath)
        {
        }

        public override void Render(ViewContext viewContext, TextWriter writer)
        {
            // there seems to be a bug with RenderPartial tainting the page's view data
            // so we should capture the current view path, and revert back after rendering
            string originalViewPath = (string)viewContext.ViewData[ResourceExtensions.ViewPathKey];

            viewContext.ViewData[ResourceExtensions.ViewPathKey] = ViewPath;
            base.Render(viewContext, writer);

            viewContext.ViewData[ResourceExtensions.ViewPathKey] = originalViewPath;
        }
    }
}