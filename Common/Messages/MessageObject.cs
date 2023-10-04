// --------------------------------------------------------------------------------
// tailor-made software GmbH
// --------------------------------------------------------------------------------
// project:      Common
// filename:     MessageObject.cs
// --------------------------------------------------------------------------------
// 
// Created:      2014-05-21   09:26
// 
// Last changed: 2014-05-21   09:32
//               Torsten Vogt (tvo)
// --------------------------------------------------------------------------------

using System.Xml;

namespace TMS.Common.Messages
{
    /// <summary>
    /// 
    /// </summary>
    public class MessageObject
    {
        /// <summary>
        /// Gets or sets the message identifier.
        /// </summary>
        /// <value>
        /// The message identifier.
        /// </value>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        public string Data { get; set; }

        ///// <summary>
        ///// Gets or sets the data text.
        ///// </summary>
        ///// <value>
        ///// The data text.
        ///// </value>
        //public string DataText
        //{
        //    set
        //    {
        //        if (Data != null && System.String.Compare(Data.InnerText, value, System.StringComparison.Ordinal) == 0)
        //            return;

        //        var doc = new XmlDocument();
        //        doc.LoadXml(value);
                
        //        Data = doc;
        //    }
        //}
        /// <summary>

        /// Clones this instance.
        /// </summary>
        /// <returns></returns>
        public MessageObject Clone()
        {
            return new MessageObject {Id = this.Id, Data = this.Data};
        }
    }
}