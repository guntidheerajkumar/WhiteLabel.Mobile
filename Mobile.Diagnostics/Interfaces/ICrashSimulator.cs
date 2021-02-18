using System;

namespace Mobile.Diagnostics.Interfaces
{
    public interface ICrashSimulator
    {
        void SimulateCrash(string crashIdentifier = null);

        void SimulateUnobservedTaskException(string exceptionIdentifier = null);
    }
}
