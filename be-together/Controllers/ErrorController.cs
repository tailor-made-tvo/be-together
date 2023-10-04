﻿// --------------------------------------------------------------------------------
// tailor-made software GmbH
// --------------------------------------------------------------------------------
// project:      be-together
// filename:     ErrorController.cs
// --------------------------------------------------------------------------------
// 
// Created:      2015-01-06   08:47
// 
// Last changed: 2015-01-06   09:02
//               Torsten Vogt (tvo)
// --------------------------------------------------------------------------------

using System.Net;
using System.Web.Mvc;
using TMS.be_together.Constants;
using TMS.be_together.Models;

namespace TMS.be_together.Controllers
{
    /// <summary>
    ///     Provides methods that respond to HTTP requests with HTTP errors.
    /// </summary>
    [RoutePrefix("error")]
    public sealed class ErrorController : Controller
    {
        #region Private Methods

        private ActionResult GetErrorView(HttpStatusCode statusCode, string viewName)
        {
            this.Response.StatusCode = (int) HttpStatusCode.NotFound;

            // Don't show IIS custom errors.
            this.Response.TrySkipIisCustomErrors = true;

            ErrorModel error = new ErrorModel()
            {
                RequestedUrl = this.Request.Url.ToString(),
                ReferrerUrl = (this.Request.UrlReferrer == null) ? null : this.Request.UrlReferrer.ToString()
            };

            ActionResult result;
            if (this.Request.IsAjaxRequest())
            {
                // This allows us to show not found errors even in partial views.
                result = this.PartialView(viewName, error);
            }
            else
            {
                result = this.View(viewName, error);
            }

            return result;
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Returns a HTTP 404 Not Found error view. Returns a partial view if the request is an AJAX call.
        /// </summary>
        /// <returns>The partial or full not found view.</returns>
        [OutputCache(CacheProfile = CacheProfileName.NotFound)]
        [Route("notfound", Name = ErrorControllerRoute.GetNotFound)]
        public ActionResult NotFound()
        {
            return this.GetErrorView(HttpStatusCode.NotFound, ErrorControllerAction.NotFound);
        }

        /// <summary>
        ///     Returns a HTTP 401 Unauthorized error view. Returns a partial view if the request is an AJAX call.
        /// </summary>
        /// <returns>The partial or full unauthorized view.</returns>
        [OutputCache(CacheProfile = CacheProfileName.Unauthorized)]
        [Route("unauthorized", Name = ErrorControllerRoute.GetUnauthorized)]
        public ActionResult Unauthorized()
        {
            return this.GetErrorView(HttpStatusCode.Unauthorized, ErrorControllerAction.Unauthorized);
        }

        #endregion
    }
}