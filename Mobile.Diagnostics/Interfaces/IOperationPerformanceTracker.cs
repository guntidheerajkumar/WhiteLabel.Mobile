using System;

namespace Mobile.Diagnostics.Interfaces
{
    public interface IOperationPerformanceTracker
    {
        void OperationStarted(string operationName);

        void OperationEnded(string operationName);
    }
}
