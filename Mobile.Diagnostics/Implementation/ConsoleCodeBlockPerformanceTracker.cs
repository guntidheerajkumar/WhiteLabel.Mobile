using System;
using System.Collections.Generic;
using System.Diagnostics;
using Mobile.Diagnostics.Interfaces;

namespace Mobile.Diagnostics.Implementation
{
    public class ConsoleCodeBlockPerformanceTracker : ICodeBlockPerformanceTracker
    {
        private readonly string _callerName;
        private readonly string _operationName;
        private readonly Stopwatch _stopwatch;

        public ConsoleCodeBlockPerformanceTracker(string callerName, string operationName, IDictionary<string, string> metadata = null)
        {
            _callerName = callerName;
            _operationName = operationName;
            _stopwatch = new Stopwatch();

            var logOutput = new string[]
            {
                $"Tracking performance from caller: {_callerName}",
                $"Operation: {_operationName}"
            };

            Debug.WriteLine(logOutput);

            //ONLY DEBUG

            //if (Logger.IsEnabled)
            //{
            // There isn't really any point running the stopwatch if we aren't logging, this is also a way
            // to ensure this doesn't accidentally get left switched on if we use this tracker in a release build.
            _stopwatch.Start();
            //}
        }

        public void AddMetadata(string key, string value)
        {
            // Not implemented in console tracker.
        }

        public void Dispose()
        {
            _stopwatch.Stop();

            var logOutput = new string[]
            {
                $"Stopped tracking performance from caller: {_callerName}",
                $"Operation: {_operationName}",
                $"Performance: {_stopwatch.ElapsedMilliseconds}ms"
            };

            Debug.WriteLine(logOutput);
        }
    }
}
