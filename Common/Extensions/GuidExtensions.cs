// --------------------------------------------------------------------------------
// tailor-made software GmbH
// --------------------------------------------------------------------------------
// project:      Common
// filename:     GuidExtensions.cs
// --------------------------------------------------------------------------------
// 
// Created:      2014-02-15   12:35
// 
// Last changed: 2014-02-15   12:36
//               Torsten Vogt (tvo)
// --------------------------------------------------------------------------------

#region

using System;

#endregion

namespace TMS.Common.Extensions
{
    /// <summary>
    ///     Defines integer extensions
    /// </summary>
    public static class GuidExtensions
    {
        /// <summary>
        ///     Produces a string, matches a guid-parameter-string
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="startText">The start text.</param>
        /// <param name="endText">The end text.</param>
        /// <returns></returns>
        public static string ToKeyString(this Guid value, string startText, string endText)
        {
            return string.Format("{0}{1}{2}", startText, value, endText);
        }

        /// <summary>
        ///     Produces a string, matches a guid-parameter-string
        ///     Additional data is binding position value
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="startText">The start text.</param>
        /// <param name="endText">The end text.</param>
        /// <param name="bindingPositionValue">The binding position value.</param>
        /// <returns></returns>
        public static string ToKeyString(this Guid value, string startText, string endText, int bindingPositionValue)
        {
            return string.Format("{0}{1}:{2}{3}", startText, bindingPositionValue, value, endText);
        }
    }
}