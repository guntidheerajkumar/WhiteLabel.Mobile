using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Mobile.Diagnostics.Interfaces;

namespace Mobile.Diagnostics.Implementation
{
    public class CrashSimulator : ICrashSimulator
    {
        public void SimulateCrash(string crashIdentifier = null)
        {
            var exceptionMessage = crashIdentifier ?? Guid.NewGuid().ToString();

            Debug.WriteLine($"Simulated Exception: {exceptionMessage}");

            throw new SimulatedCrashException($"Simulated Exception: {exceptionMessage}");
        }

        public void SimulateUnobservedTaskException(string exceptionIdentifier = null)
        {
            var exceptionMessage = exceptionIdentifier ?? Guid.NewGuid().ToString();

            Debug.WriteLine($"Simulated Unobserved Task Exception: {exceptionMessage}");

            Action errorAction = () =>
            {
                // Create a task that will throw an unhandled exception, this task will run out of scope at the end
                // of the action and become eligible for garbage collection.
                var errorTask = Task.Run(() =>
                {
                    throw new SimulatedCrashException($"Simulated Unobserved Task Exception: {exceptionMessage}");
                });

                // We need to awit for the task to finish but we can't await it because we want the exception
                // to be unhandled.
                ((IAsyncResult)errorTask).AsyncWaitHandle.WaitOne();
            };

            errorAction.Invoke();

            // The unhandled exception won't be propagated until the task is finalized so we're manually invoking
            // the garbage collector to force that to happen.
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}
