// --------------------------------------------------------------------------------
// tailor-made software GmbH
// --------------------------------------------------------------------------------
// project:      Common
// filename:     IntExtensions.cs
// --------------------------------------------------------------------------------
// 
// Created:      2014-02-15   12:35
// 
// Last changed: 2014-02-15   12:36
//               Torsten Vogt (tvo)
// --------------------------------------------------------------------------------

#region



#endregion

namespace TMS.Common.Extensions
{
    /// <summary>
    ///     Defines integer extensions
    /// </summary>
    public static class IntExtensions
    {
        /// <summary>
        ///     Changes the value to the given <seealso cref="StringSizingEnum" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="sizing">The sizing.</param>
        /// <returns></returns>
        public static int ToBase(this int value, StringSizingEnum sizing)
        {
            var ret = value;

            switch (sizing)
            {
                case StringSizingEnum.KB:
                    ret = ret*1024;
                    break;
                case StringSizingEnum.MB:
                    ret = ret*1048576;
                    break;
            }
            return ret;
        }

        /// <summary>
        ///     Converts an integer to hexadecimal digits within 0x???????? format.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string ToHex(this int value)
        {
            return string.Format("0x{0:X8}", value);
        }
    }
}