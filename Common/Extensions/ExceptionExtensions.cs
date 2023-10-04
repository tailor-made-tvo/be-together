// --------------------------------------------------------------------------------
// tailor-made software GmbH
// --------------------------------------------------------------------------------
// project:      Common
// filename:     ExceptionExtensions.cs
// --------------------------------------------------------------------------------
// 
// Created:      2014-02-15   12:35
// 
// Last changed: 2014-02-15   12:35
//               Torsten Vogt (tvo)
// --------------------------------------------------------------------------------

#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

#endregion

namespace TMS.Common.Extensions
{
    /// <summary>
    ///     Simplifies adding of exception detail data.
    /// </summary>
    public static class ExceptionExtensions
    {
        /// <summary>
        ///     Allows adding of a key value pair to the Exception.Data dictionary.
        ///     If the key already exists, another key entry with an incremented name is added.
        /// </summary>
        public static void DataAddOrAppend(this Exception ex, string key, object value)
        {
            var keyData = key;
            var i = 0;
            while (ex.Data.Contains(keyData))
            {
                i++;
                keyData = String.Format("{0}-{1}",
                    key,
                    i);
            }
            ex.Data.Add(keyData,
                (value ?? string.Empty).ToString());
        }

        /// <summary>
        ///     Allows adding of a key value pair to the Exception.Data dictionary.
        ///     If the key already exists, another key entry with an incremented name is added.
        /// </summary>
        public static void DataAddOrAppend(this Exception ex, IDictionary<string, object> data)
        {
            foreach (var d in data)
                ex.DataAddOrAppend(d.Key, d.Value);
        }

        /// <summary>
        ///     Gets the exception data.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <returns></returns>
        public static string GetExceptionData(this Exception ex)
        {
            if (ex == null)
                return string.Empty;

            var sb = new StringBuilder();

            if (ex.Data.Count > 0)
            {
                foreach (DictionaryEntry exData in ex.Data)
                {
                    sb.AppendLine(string.Format("    {0}= \"{1}\"",
                        exData.Key,
                        exData.Value));
                }
            }

            var innerException = ex.InnerException;
            while (innerException != null)
            {
                sb.AppendLine("\r\nInnerExceptionData:");
                foreach (DictionaryEntry exData in innerException.Data)
                {
                    sb.AppendLine(string.Format("    {0}= \"{1}\"",
                        exData.Key,
                        exData.Value));
                }

                innerException = innerException.InnerException;
            }

            return sb.ToString();
        }
    }
}