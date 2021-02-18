using System;

namespace Mobile.Diagnostics.Models
{
    public class DiagnosticsConfiguration
    {
        public DiagnosticTool Tool { get; set; }

        public string AppKey { get; set; }

        public bool IsLoggingEnabled { get; set; }
    }
}
