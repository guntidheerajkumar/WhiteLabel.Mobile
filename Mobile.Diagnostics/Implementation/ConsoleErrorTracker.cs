using System;
using System.Collections.Generic;
using Mobile.Diagnostics.Interfaces;

namespace Mobile.Diagnostics.Implementation
{
    public class ConsoleErrorTracker : IErrorTracker
    {
        public void TrackError(Exception exception, IDictionary<string, string> metadata = null) =>
            System.Diagnostics.Debug.WriteLine("Error reported to Error Tracker", exception);
    }
}
