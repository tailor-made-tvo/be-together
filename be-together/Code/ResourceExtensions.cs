using System.Globalization;
using System.Web;
using System.Web.Compilation;
using System.Web.Mvc;

namespace TMS.be_together.Code
{
    public static class ResourceExtensions
    {
        public const string ViewPathKey = "__ViewPath__";
        //public const string LayoutPathKey = "__LayoutPath__";
        //public const string RunViewStartPagesKey = "__RunViewStartPages__";
        //public const string ViewStartFileExtensionsKey = "__ViewStartFileExtensions__";

        public static string Resource(this string expression, params object[] args)
        {
            ResourceExpressionFields fields = GetResourceFields(expression, "~/");
            return GetGlobalResource(fields, args);
        }

        public static string Resource(this Controller controller, string expression, params object[] args)
        {
            ResourceExpressionFields fields = GetResourceFields(expression, "~/");
            return GetGlobalResource(fields, args);
        }

        public static string Resource(this HtmlHelper htmlHelper, string expression, params object[] args)
        {
            string path = (string)htmlHelper.ViewData[ViewPathKey];
            if (string.IsNullOrEmpty(path))
                path = "~/";

            ResourceExpressionFields fields = GetResourceFields(expression, path);
            if (!string.IsNullOrEmpty(fields.ClassKey))
                return GetGlobalResource(fields, args);

            return GetLocalResource(path, fields, args);
        }

        static string GetLocalResource(string path, ResourceExpressionFields fields, object[] args)
        {
            //return string.Format((string)HttpContext.GetLocalResourceObject(path, fields.ResourceKey, CultureInfo.CurrentUICulture), args);
            return string.Format((string)HttpContext.GetLocalResourceObject(path, fields.ResourceKey, new CultureInfo(LanguageHelper.GetLanguage())), args);
        }

        static string GetGlobalResource(ResourceExpressionFields fields, object[] args)
        {
            //return string.Format((string)HttpContext.GetGlobalResourceObject(fields.ClassKey, fields.ResourceKey, CultureInfo.CurrentUICulture), args);
            return string.Format((string)HttpContext.GetGlobalResourceObject(fields.ClassKey, fields.ResourceKey, new CultureInfo(LanguageHelper.GetLanguage())), args);
        }

        static ResourceExpressionFields GetResourceFields(string expression, string virtualPath)
        {
            var context = new ExpressionBuilderContext(virtualPath);
            var builder = new ResourceExpressionBuilder();
            return (ResourceExpressionFields)builder.ParseExpression(expression, typeof(string), context);
        }
    }
}