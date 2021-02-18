using System;
using System.Collections.Generic;

namespace Mobile.Diagnostics.Interfaces
{
    public interface IErrorTracker
    {
        void TrackError(Exception exception, IDictionary<string, string> metadata = null);
    }
}
