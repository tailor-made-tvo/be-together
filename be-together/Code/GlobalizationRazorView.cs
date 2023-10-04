using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using System.Web.Mvc;


namespace TMS.be_together.Code
{
    public class GlobalizationRazorView : RazorView
    {
        public GlobalizationRazorView(ControllerContext controllerContext, string viewPath, string layoutPath, bool runViewStartPages, IEnumerable<string> viewStartFileExtensions)
            : base(controllerContext, viewPath, layoutPath, runViewStartPages, viewStartFileExtensions)
        {
        }

        public GlobalizationRazorView(ControllerContext controllerContext, string viewPath, string layoutPath, bool runViewStartPages, IEnumerable<string> viewStartFileExtensions, IViewPageActivator viewPageActivator)
            : base(controllerContext, viewPath, layoutPath, runViewStartPages, viewStartFileExtensions, viewPageActivator)
        {
        }

        public override void Render(ViewContext viewContext, TextWriter writer)
        {
            // there seems to be a bug with RenderPartial tainting the page's view data
            // so we should capture the current view path, and revert back after rendering
            var originalViewPath = (string)viewContext.ViewData[ResourceExtensions.ViewPathKey];
            //var originalLayoutPath = (string)viewContext.ViewData[LayoutPathKey];
            //var originalRunViewStartPages = (bool)(viewContext.ViewData[RunViewStartPagesKey] ?? false);
            //var originalViewStartFileExtensions = (string[])viewContext.ViewData[ViewStartFileExtensionsKey];

            viewContext.ViewData[ResourceExtensions.ViewPathKey] = ViewPath;
            //viewContext.ViewData[LayoutPathKey] = LayoutPath;
            //viewContext.ViewData[RunViewStartPagesKey] = RunViewStartPages;
            //viewContext.ViewData[ViewStartFileExtensionsKey] = ViewStartFileExtensions;
            base.Render(viewContext, writer);

            viewContext.ViewData[ResourceExtensions.ViewPathKey] = originalViewPath;
            //viewContext.ViewData[LayoutPathKey] = originalLayoutPath;
            //viewContext.ViewData[RunViewStartPagesKey] = originalRunViewStartPages;
            //viewContext.ViewData[ViewStartFileExtensionsKey] = originalViewStartFileExtensions;
        }
    }
}