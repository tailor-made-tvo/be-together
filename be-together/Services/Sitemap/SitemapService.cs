﻿// --------------------------------------------------------------------------------
// tailor-made software GmbH
// --------------------------------------------------------------------------------
// project:      be-together
// filename:     SitemapService.cs
// --------------------------------------------------------------------------------
// 
// Created:      2015-01-06   08:47
// 
// Last changed: 2015-01-06   09:03
//               Torsten Vogt (tvo)
// --------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using System.Xml.Linq;
using TMS.be_together.Constants;
using TMS.be_together.Framework;

// ReSharper disable once CheckNamespace
namespace TMS.be_together.Services
{
    /// <summary>
    ///     Generates sitemap XML for the current site.
    /// </summary>
    public sealed class SitemapService : ISitemapService
    {
        private const string SitemapsNamespace = "http://www.sitemaps.org/schemas/sitemap/0.9";
        private const int MaximumSitemapNodeCount = 50000;
        private readonly ILoggingService loggingService;
        private readonly UrlHelper urlHelper;

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="SitemapService" /> class.
        /// </summary>
        /// <param name="loggingService">The logging service.</param>
        /// <param name="urlHelper">The URL helper.</param>
        public SitemapService(ILoggingService loggingService, UrlHelper urlHelper)
        {
            this.loggingService = loggingService;
            this.urlHelper = urlHelper;
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Gets the sitemap XML for the current site. If there are more than 50,000 sitemap nodes
        ///     (The maximum allowed before you have to use a sitemap index file), the list is truncated and an error is logged.
        ///     See http://www.sitemaps.org/protocol.html for more information.
        /// </summary>
        /// <returns>The sitemap XML for the current site.</returns>
        public string GetSitemapXml()
        {
            IReadOnlyCollection<SitemapNode> sitemapNotes = this.GetSitemapNodes();

            if (sitemapNotes.Count > MaximumSitemapNodeCount)
            {
                OverflowException overflowException =
                    new OverflowException(
                        string.Format(
                            "There are too many sitemap nodes, the collection has been truncated. If you want to use more than 50,000, you must use a sitemap index file (See http://www.sitemaps.org/protocol.html). Count:<{0}>.",
                            sitemapNotes.Count));
                this.loggingService.Log(overflowException);
            }

            XNamespace xmlns = SitemapsNamespace;
            XElement root = new XElement(xmlns + "urlset");
            foreach (SitemapNode sitemapNode in sitemapNotes.Take(MaximumSitemapNodeCount))
            {
                root.Add(new XElement(xmlns + "url", new XElement(xmlns + "loc", Uri.EscapeUriString(sitemapNode.Url)),
                    sitemapNode.LastModified == null
                        ? null
                        : new XElement(xmlns + "lastmod",
                            sitemapNode.LastModified.Value.ToLocalTime().ToString("yyyy-MM-ddTHH:mm:sszzz")),
                    sitemapNode.Frequency == null
                        ? null
                        : new XElement(xmlns + "changefreq", sitemapNode.Frequency.Value.ToString().ToLowerInvariant()),
                    sitemapNode.Priority == null
                        ? null
                        : new XElement(xmlns + "priority",
                            sitemapNode.Priority.Value.ToString("F1", CultureInfo.InvariantCulture))));
            }

            return root.ToString();
        }

        #endregion

        #region Private Methods

        /// <summary>
        ///     Gets a collection of sitemap nodes for the current site.
        ///     TODO: Add code here to create nodes to all your important sitemap URL's.
        ///     You may want to do this from a database or in code.
        /// </summary>
        /// <returns>A collection of sitemap nodes for the current site.</returns>
        private IReadOnlyCollection<SitemapNode> GetSitemapNodes()
        {
            List<SitemapNode> nodes = new List<SitemapNode>();

            nodes.Add(new SitemapNode(this.urlHelper.AbsoluteRouteUrl(HomeControllerRoute.GetIndex)) { Priority = 1 });

            return nodes;
        }

        #endregion
    }
}