﻿// --------------------------------------------------------------------------------
// tailor-made software GmbH
// --------------------------------------------------------------------------------
// project:      be-together
// filename:     UrlHelperExtensions.cs
// --------------------------------------------------------------------------------
// 
// Created:      2015-01-06   08:47
// 
// Last changed: 2015-01-06   09:04
//               Torsten Vogt (tvo)
// --------------------------------------------------------------------------------

using System.Web.Mvc;

namespace TMS.be_together.Framework
{
    /// <summary>
    ///     <see cref="UrlHelper" /> extension methods.
    /// </summary>
    public static class UrlHelperExtensions
    {
        /// <summary>
        ///     Generates a fully qualified URL to an action method by using
        ///     the specified action name, controller name and route values.
        /// </summary>
        /// <param name="url">The URL helper.</param>
        /// <param name="actionName">The name of the action method.</param>
        /// <param name="controllerName">The name of the controller.</param>
        /// <param name="routeValues">The route values.</param>
        /// <returns>The absolute URL.</returns>
        public static string AbsoluteAction(this UrlHelper url,
            string actionName,
            string controllerName,
            object routeValues = null)
        {
            string scheme = url.RequestContext.HttpContext.Request.Url.Scheme;
            return url.Action(actionName, controllerName, routeValues, scheme);
        }

        /// <summary>
        ///     Generates a fully qualified URL to the specified route by using
        ///     the route name and route values.
        /// </summary>
        /// <param name="url">The URL helper.</param>
        /// <param name="routeName">Name of the route.</param>
        /// <param name="routeValues">The route values.</param>
        /// <returns>The absolute URL.</returns>
        public static string AbsoluteRouteUrl(this UrlHelper url, string routeName, object routeValues = null)
        {
            string scheme = url.RequestContext.HttpContext.Request.Url.Scheme;
            return url.RouteUrl(routeName, routeValues, scheme);
        }
    }
}