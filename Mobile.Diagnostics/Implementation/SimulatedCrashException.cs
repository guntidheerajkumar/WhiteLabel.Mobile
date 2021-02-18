using System;
namespace Mobile.Diagnostics.Implementation
{
    public class SimulatedCrashException : Exception
    {
        public SimulatedCrashException(string message)
            : base(message)
        {
            // This class has no functionality it acts as a marker in crash reporting 
            // tools so that crashes generated through testing can be identified.
        }
    }
}
