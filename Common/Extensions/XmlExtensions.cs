// --------------------------------------------------------------------------------
// tailor-made software GmbH
// --------------------------------------------------------------------------------
// project:      Common
// filename:     XmlExtensions.cs
// --------------------------------------------------------------------------------
// 
// Created:      2014-02-15   12:35
// 
// Last changed: 2014-02-15   12:38
//               Torsten Vogt (tvo)
// --------------------------------------------------------------------------------

#region

using System.Xml;

#endregion

namespace TMS.Common.Extensions
{
    /// <summary>
    /// </summary>
    public static class XmlExtensions
    {
        /// <summary>
        ///     Gets the attribute value.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="attributeName">Name of the attribute.</param>
        /// <returns></returns>
        public static string GetAttributeValue(this XmlNode node, string attributeName)
        {
            return GetAttributeValue(node, attributeName, string.Empty);
        }

        /// <summary>
        ///     Gets the attribute value.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="attributeName">Name of the attribute.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static string GetAttributeValue(this XmlNode node, string attributeName, string defaultValue)
        {
            var ret = defaultValue;

            if (node.Attributes != null)
            {
                var websiteAttribute = node.Attributes[attributeName];
                if (websiteAttribute != null)
                {
                    ret = websiteAttribute.Value.TrimToSingleLine(); //.Url2AsciiEncoding();
                }
            }

            return ret;
        }
    }
}