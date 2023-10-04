// --------------------------------------------------------------------------------
// tailor-made software GmbH
// --------------------------------------------------------------------------------
// project:      Common
// filename:     ObjectExtensions.cs
// --------------------------------------------------------------------------------
// 
// Created:      2014-02-15   12:35
// 
// Last changed: 2014-02-15   12:37
//               Torsten Vogt (tvo)
// --------------------------------------------------------------------------------

#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

#endregion

namespace TMS.Common.Extensions
{
    /// <summary>
    /// Devines object extensions
    /// </summary>
    public static class ObjectExtensions
    {
        // Currently not used!
        ///// <summary>
        ///// Does the generic.
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="value">The value.</param>
        ///// <param name="defaultValue">The default value.</param>
        ///// <returns></returns>
        ///// <remarks></remarks>
        //public static T ToGeneric<T>(this object value,
        //                             T defaultValue)
        //{
        //    if (value != null)
        //    {
        //        if (value is T)
        //            return (T) value;

        //        if (value is string)
        //        {
        //            if (typeof (T) ==
        //                typeof (string))
        //            {
        //                return (T) (object) value.ToString();
        //            }

        //            if (defaultValue is bool)
        //            {
        //                bool ret;

        //                if (bool.TryParse((string) value,
        //                                  out ret))
        //                    return (T) (object) ret;
        //            }

        //            if (defaultValue is int)
        //            {
        //                int ret;

        //                if (int.TryParse((string) value,
        //                                 out ret))
        //                    return (T) (object) ret;
        //            }
        //        }
        //    }

        //    return defaultValue;
        //}

        ///// <summary>
        ///// Returns a <see cref="System.String"/> that represents this instance.
        ///// </summary>
        ///// <param name="value">The value.</param>
        ///// <returns>
        ///// A <see cref="System.String"/> that represents this instance.
        ///// </returns>
        //public static string ToString(this object value)
        //{
        //    if (value is byte[])
        //    {
        //        var ret = new StringBuilder();
        //        var bytes = value as byte[];
        //        foreach (var b in bytes)
        //        {
        //            ret.AppendFormat("{0}", (char)b);
        //        }

        //        return ret.ToString();
        //    }

        //    return value.ToString();
        //}

        /// <summary>
        ///     Returns a <see cref="System.String" /> which comes from a byte array.
        /// </summary>
        /// <param name="bytes">The bytes-array.</param>
        /// <param name="count">The count of bytes which must be converted to string.</param>
        /// <returns>
        ///     A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public static string ToString(this byte[] bytes, int count)
        {
            var ret = new StringBuilder();
            for (var i = 0; i < bytes.Length && (count <= 0 || count > i); i++)
                ret.Append((char) bytes[i]);

            return ret.ToString();
        }

        /// <summary>
        ///     Converts a the data of <seealso cref="value" /> to a distinct Dictionary.
        /// </summary>
        /// <param name="value">The list of key value pairs.</param>
        /// <returns>A distinct, per key, Dictionary.</returns>
        public static Dictionary<string, string> ToDistinctDictionary(this List<KeyValuePair<string, string>> value)
        {
            var ret = new Dictionary<string, string>();

            foreach (var data in value.Where(data => !ret.ContainsKey(data.Key)))
            {
                ret.Add(data.Key,
                    data.Value);
            }

            return ret;
        }

        /// <summary>
        ///     Adds all data to given Dictionary and overrides it, when the data exists
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="addList">The add list.</param>
        public static void AddRange(this IDictionary<string, object> value, IDictionary<string, object> addList)
        {
            foreach (var data in addList)
            {
                value[data.Key] = data.Value;
            }
        }

        /// <summary>
        ///     Converts a <see cref="IDictionary" /> to Dictionary&lt;string, object&gt;.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="sort">if set to <c>true</c> [sort].</param>
        /// <returns></returns>
        public static Dictionary<string, object> ToDictionaryData(this IDictionary data, bool sort = false)
        {
            var ret = new Dictionary<string, object>();

            if (data == null || data.Count == 0)
                return ret;

            foreach (var k in data.Keys)
                ret[k.ToString()] = data[k];

            return sort ? ret.OrderBy(v => v.Key).ToDictionary(k => k.Key, v => v.Value) : ret;
        }

        /// <summary>
        ///     Prepares the variables.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public static Dictionary<string, object> PrepareReplacementVariables(this Dictionary<string, object> data)
        {
            var ret = new Dictionary<string, object>();
            if (data == null)
                return ret;

            foreach (var d in data)
            {
                //var key = d.Key.Trim().ReplaceRegEx( new Dictionary<string, object> {{@"[^0-9a-zA-Z_\.\-\\]", "_"}, {@"\.", @"\."}, {@"\-", @"\-"}, {"__", "_"}},
                //                                     RegexOptions.Compiled );
                var key =
                    d.Key.Trim(new[] {' ', '-', '_', '.'})
                        .ReplaceRegEx(new Dictionary<string, object> {{@"[^0-9a-zA-Z_\.\-]", ""}}, RegexOptions.Compiled);

                ret[key] = d.Value;
            }

            return ret;
        }

        /// <summary>
        /// Gets the first attribute of given type.
        /// </summary>
        /// <typeparam name="T">Type of the attribute to search</typeparam>
        /// <param name="objectItem">The object item.</param>
        /// <returns></returns>
        public static T GetCustomAttribute<T>(this object objectItem) where T : Attribute
        {
            return GetCustomAttribute<T>(objectItem, false);
        }

        /// <summary>
        /// Gets the first attribute of given type.
        /// </summary>
        /// <typeparam name="T">Type of the attribute to search</typeparam>
        /// <param name="objectItem">The object item.</param>
        /// <param name="inherit"><c>true</c> to search this member's inheritance chain to find the attributes; otherwise, <c>false</c>. This parameter is ignored for properties and events.</param>
        /// <returns></returns>
        public static T GetCustomAttribute<T>(this object objectItem, bool inherit) where T : Attribute
        {
            T ret = null;
            var attributes = objectItem.GetType().GetCustomAttributes(typeof(T), inherit);

            if (attributes.Length == 1)
            {
                ret = (T)attributes[0];
            }

            return ret;
        }

        /// <summary>
        /// Gets all attributes of given type.
        /// </summary>
        /// <typeparam name="T">Type of the attribute to search</typeparam>
        /// <param name="objectItem">The object item.</param>
        /// <returns></returns>
        public static List<T> GetCustomAttributes<T>(this object objectItem) where T : Attribute
        {
            return GetCustomAttributes<T>(objectItem, false);
        }

        /// <summary>
        /// Gets all attributes of given type.
        /// </summary>
        /// <typeparam name="T">Type of the attribute to search</typeparam>
        /// <param name="objectItem">The object item.</param>
        /// <param name="inherit"><c>true</c> to search this member's inheritance chain to find the attributes; otherwise, <c>false</c>. This parameter is ignored for properties and events.</param>
        /// <returns></returns>
        public static List<T> GetCustomAttributes<T>(this object objectItem, bool inherit) where T : Attribute
        {
            var attributes = objectItem.GetType().GetCustomAttributes(typeof(T), inherit);

            return attributes.Cast<T>().ToList();
        }
    }
}