// --------------------------------------------------------------------------------
// tailor-made software GmbH
// --------------------------------------------------------------------------------
// project:      be-together
// filename:     ISitemapService.cs
// --------------------------------------------------------------------------------
// 
// Created:      2015-01-06   08:47
// 
// Last changed: 2015-01-06   09:03
//               Torsten Vogt (tvo)
// --------------------------------------------------------------------------------

// ReSharper disable once CheckNamespace
namespace TMS.be_together.Services
{
    public interface ISitemapService
    {
        /// <summary>
        ///     Gets the sitemap XML for the current site. If there are more than 50,000 sitemap nodes
        ///     (The maximum allowed before you have to use a sitemap index file), the list is truncated and an error is logged.
        ///     See http://www.sitemaps.org/protocol.html for more information.
        /// </summary>
        /// <returns>The sitemap XML for the current site.</returns>
        string GetSitemapXml();
    }
}