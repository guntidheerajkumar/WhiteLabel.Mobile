using System;
using System.Diagnostics;
using Serilog.Core;
using Serilog.Events;

namespace Mobile.Diagnostics.Implementation
{
    public class ConsoleSink : ILogEventSink
    {
        public void Emit(LogEvent logEvent)
        {
            // For this package we define the DEBUG symbol in all build configurations.
            // Logging should be enabled or disabled within apps using the IsEnabled property of Logger.
            Debug.WriteLine($"{logEvent.RenderMessage()}");
        }
    }
}
