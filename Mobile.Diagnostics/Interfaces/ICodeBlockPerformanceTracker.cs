using System;

namespace Mobile.Diagnostics.Interfaces
{
    public interface ICodeBlockPerformanceTracker: IDisposable
    {
        void AddMetadata(string key, string value);
    }
}