// --------------------------------------------------------------------------------
// tailor-made software GmbH
// --------------------------------------------------------------------------------
// project:      be-together
// filename:     LoggingService.cs
// --------------------------------------------------------------------------------
// 
// Created:      2015-01-06   08:47
// 
// Last changed: 2015-01-06   09:03
//               Torsten Vogt (tvo)
// --------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using Elmah;

// ReSharper disable once CheckNamespace
namespace TMS.be_together.Services
{
    public sealed class LoggingService : ILoggingService
    {
        public void Log(Exception exception)
        {
            Trace.TraceError(exception.ToString());
            ErrorSignal.FromCurrentContext().Raise(exception);
        }
    }
}