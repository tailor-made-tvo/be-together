// --------------------------------------------------------------------------------
// tailor-made software GmbH
// --------------------------------------------------------------------------------
// project:      Common
// filename:     ViewModelBase.cs
// --------------------------------------------------------------------------------
// 
// Created:      2014-05-21   08:06
// 
// Last changed: 2014-05-21   08:16
//               Torsten Vogt (tvo)
// --------------------------------------------------------------------------------

#region

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

#endregion

namespace TMS.Common.ViewModels
{
    /// <summary>
    /// Represents the bas class of all ViewModels.
    /// It implements the <see cref="INotifyPropertyChanged"/> members and holds all of it's variables in a Dictionary.
    /// </summary>
    public class ViewModelBase : INotifyPropertyChanged, IDisposable
    {
        private readonly Dictionary<string, object> _propertyValues;

        #region ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModelBase"/> class.
        /// </summary>
        public ViewModelBase()
        {
            _propertyValues = new Dictionary<string, object>();
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="ViewModelBase"/> class.
        /// </summary>
        ~ViewModelBase()
        {
            Dispose(false);
        }

        #endregion

        #region INotifyPropertyChanged Members

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Sets the specified property by its name.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <param name="name">The name.</param>
        /// <example>
        /// public long Id
        /// {
        ///     get { return Get&lt;long&gt;(); }
        ///     set { Set(value); }
        /// }
        /// </example>
        protected void Set<T>(T value, [CallerMemberName] string name = "")
        {
            if (_propertyValues.ContainsKey(name))
            {
                var oldValue = _propertyValues[name];
                if (!oldValue.Equals(value))
                {
                    _propertyValues[name] = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs(name));
                }
            }
            else
            {
                _propertyValues.Add(name, value);
            }
        }

        /// <summary>
        /// Gets the specified property by it's name and type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        protected T Get<T>([CallerMemberName] string name = "")
        {
            if (_propertyValues.ContainsKey(name))
                return (T) _propertyValues[name];
            
            return default (T);
        }

        #endregion

        #region IDisposable Members

        private bool _disposed;

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            //if (!_disposed)
            //{
            //    if (disposing)
            //    {
            //        Close();
            //    }
            //}
            _disposed = true;
        }

        #endregion
    }
}