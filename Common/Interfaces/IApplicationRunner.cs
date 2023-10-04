#region

using System.ComponentModel;
using TMS.Common.Messages;

#endregion



namespace TMS.Common.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IApplicationRunner
    {
        /// <summary>
        /// Runs the specified message object.
        /// </summary>
        /// <param name="messageObject">The message object.</param>
        void Run(MessageObject messageObject);
    }
}
