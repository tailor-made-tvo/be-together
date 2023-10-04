// --------------------------------------------------------------------------------
// tailor-made software GmbH
// --------------------------------------------------------------------------------
// project:      Common
// filename:     VariablesObject.cs
// --------------------------------------------------------------------------------
// 
// Created:      2014-11-18   22:47
// 
// Last changed: 2014-11-18   23:35
//               Torsten Vogt (tvo)
// --------------------------------------------------------------------------------

#region

using System.Collections.Generic;
using TMS.Common.Enumerations;

#endregion

namespace TMS.Common.Messages
{
    /// <summary>
    /// </summary>
    public class VariablesObject : Dictionary<string, string>
    {
        /// <summary>
        ///     Gets or sets the <see cref="System.String" /> with the specified key.
        /// </summary>
        /// <value>
        ///     The <see cref="System.String" />.
        /// </value>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public string this[MessageNameEnum key]
        {
            get { return this[key.ToString()]; }
            set { this[key.ToString()] = value; }
        }

        /// <summary>
        ///     Adds the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public void Add(MessageNameEnum key, string value)
        {
            this.Add(key.ToString(), value);
        }

        /// <summary>
        ///     Determines whether the specified key contains key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public bool ContainsKey(MessageNameEnum key)
        {
            return this.ContainsKey(key.ToString());
        }

        /// <summary>
        ///     Removes the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public bool Remove(MessageNameEnum key)
        {
            return this.Remove(key.ToString());
        }

        /// <summary>
        ///     Tries the get value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public void TryGetValue(MessageNameEnum key, out string value)
        {
            this.TryGetValue(key.ToString(), out value);
        }

        /// <summary>
        ///     Clones this instance.
        /// </summary>
        /// <returns></returns>
        public VariablesObject Clone()
        {
            var ret = new VariablesObject();

            foreach (var data in this)
            {
                ret.Add(data.Key, data.Value);
            }

            return ret;
        }

        //public VariablesObject Clone()
        //{
        //    var ret = new VariablesObject();

        //    foreach (var data in this)
        //    {
        //        ret.AddOrUpdate(data.Key, data.Value, (oldKey, oldValue) => data.Value);
        //    }

        //    return ret;
        //}
    }
}