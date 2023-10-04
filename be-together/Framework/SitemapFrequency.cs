// --------------------------------------------------------------------------------
// tailor-made software GmbH
// --------------------------------------------------------------------------------
// project:      be-together
// filename:     SitemapFrequency.cs
// --------------------------------------------------------------------------------
// 
// Created:      2015-01-06   08:47
// 
// Last changed: 2015-01-06   09:03
//               Torsten Vogt (tvo)
// --------------------------------------------------------------------------------

namespace TMS.be_together.Framework
{
    /// <summary>
    ///     How frequently the page or URL is likely to change.
    /// </summary>
    public enum SitemapFrequency
    {
        /// <summary>
        ///     Describes archived URLs that never change.
        /// </summary>
        Never,

        /// <summary>
        ///     Describes URL's that change yearly.
        /// </summary>
        Yearly,

        /// <summary>
        ///     Describes URL's that change monthly.
        /// </summary>
        Monthly,

        /// <summary>
        ///     Describes URL's that change weekly.
        /// </summary>
        Weekly,

        /// <summary>
        ///     Describes URL's that change daily.
        /// </summary>
        Daily,

        /// <summary>
        ///     Describes URL's that change hourly.
        /// </summary>
        Hourly,

        /// <summary>
        ///     Describes documents that change each time they are accessed.
        /// </summary>
        Always
    }
}