using System;
using System.IO;
using System.Runtime.CompilerServices;
using Serilog;
using Serilog.Context;
using Serilog.Events;
using Serilog.Formatting.Json;

namespace Mobile.Diagnostics.Implementation
{
    public static class Logger
    {
        private const string CSharpFileExtension = ".cs";
        private const string LogFolderName = "Logs";
        private const string LogFileName = "log.txt";
        private const string RawEventsFileName = "raw.json";
        private const string ClassPropertyName = "Class";
        private const string MethodPropertyName = "Method";
        private const string LineNumberPropertyName = "LineNumber";
        private const string StandaloneFolderName = "Standalone";
        private const long FileSizeLimitBytes = 5242880;

        public static bool IsEnabled => IsEnabledInternal;

        private static readonly object LockObject = new object();

        private static bool IsEnabledInternal { get; set; } = false;

        private static bool IsLoggerConfigured { get; set; } = false;

        private static Func<bool> IsExternalFileAccessAvailable = () => false;

        private static Func<string, string> GetExternalDirectoryPath = (directoryPath) => string.Empty;

        public static void EnableLogging(Func<bool> isExternalFileAccessAvailable, Func<string, string> getExternalDirectoryPath)
        {
            lock (LockObject)
            {
                IsEnabledInternal = true;
                IsExternalFileAccessAvailable = isExternalFileAccessAvailable;
                GetExternalDirectoryPath = getExternalDirectoryPath;
            }
        }

        public static void DisableLogging()
        {
            lock (LockObject)
            {
                IsEnabledInternal = false;
                IsExternalFileAccessAvailable = () => false;
                GetExternalDirectoryPath = (directoryPath) => string.Empty;

                Serilog.Log.CloseAndFlush();
            }
        }

        public static void Log(string message, [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
        {
            if (IsEnabledInternal)
            {
                ConfigureLogging();

                var frmClass = GetClassNameFromFilePath(filePath);

                using (LogContext.PushProperty(ClassPropertyName, frmClass))
                using (LogContext.PushProperty(MethodPropertyName, memberName))
                using (LogContext.PushProperty(LineNumberPropertyName, lineNumber))
                {
                    Serilog.Log.Write(LogEventLevel.Information, $"[{frmClass}] [{memberName}.{lineNumber}] {message}");
                }
            }
        }

        public static void Log(string[] messages, [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
        {
            var combinedMesages = string.Join(Environment.NewLine, messages);
            Log(combinedMesages, memberName, filePath, lineNumber);
        }

        public static void Log(string message, Exception ex, [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
        {
            if (IsEnabledInternal)
            {
                ConfigureLogging();

                var errorMessage = string.Join(Environment.NewLine, message, ex.ToString());
                var frmClass = GetClassNameFromFilePath(filePath);

                using (LogContext.PushProperty(ClassPropertyName, frmClass))
                using (LogContext.PushProperty(MethodPropertyName, memberName))
                using (LogContext.PushProperty(LineNumberPropertyName, lineNumber))
                {
                    Serilog.Log.Write(LogEventLevel.Error, ex, $"[{frmClass}] [{memberName}.{lineNumber}] {errorMessage}");
                }
            }
        }

        public static void LogStandaloneResponse(string fileName, string fileContents)
        {
            if (IsEnabledInternal && IsExternalFileAccessAvailable?.Invoke() == true)
            {
                //var standaloneDirectory = GetExternalDirectoryPath?.Invoke(StandaloneFolderName);

                //if (standaloneDirectory != null && standaloneDirectory != string.Empty)
                //{
                //    try
                //    {
                //        if (!Directory.Exists(standaloneDirectory))
                //        {
                //            Directory.CreateDirectory(standaloneDirectory);
                //        }

                //        File.WriteAllText(Path.Combine(standaloneDirectory, $"{fileName}.json"), fileContents);
                //    }
                //    catch (Exception ex)
                //    {
                //        Log("Error logging standalone response", ex);
                //    }
                //}
            }
        }

        private static void ConfigureLogging()
        {
            lock (LockObject)
            {
                if (!IsLoggerConfigured)
                {
                    //var loggerConfiguration = new LoggerConfiguration().WriteTo.Sink(new ConsoleSink());

                    //if (IsExternalFileAccessAvailable?.Invoke() == true)
                    //{
                    //    var logFilePath = GetExternalDirectoryPath?.Invoke(Path.Combine(LogFolderName, LogFileName));

                    //    if (logFilePath != null && logFilePath != string.Empty)
                    //    {
                    //        loggerConfiguration = loggerConfiguration.WriteTo.File(logFilePath, rollingInterval:
                    //            RollingInterval.Day, fileSizeLimitBytes: FileSizeLimitBytes);
                    //    }

                    //    var rawEventPath = GetExternalDirectoryPath?.Invoke(Path.Combine(LogFolderName, RawEventsFileName));

                    //    if (rawEventPath != null && rawEventPath != string.Empty)
                    //    {
                    //        loggerConfiguration = loggerConfiguration.WriteTo.File(new JsonFormatter(), rawEventPath,
                    //            rollingInterval: RollingInterval.Day, fileSizeLimitBytes: FileSizeLimitBytes);
                    //    }
                    //}

                    //Serilog.Log.Logger = loggerConfiguration
                    //    .Enrich.FromLogContext()
                    //    .Enrich.WithThreadId()
                    //    .CreateLogger();

                    //IsLoggerConfigured = true;
                }
            }
        }

        private static string GetClassNameFromFilePath(string filePath)
        {
            var className = string.Empty;

            //var indexOfClassNameStart = filePath.LastIndexOf(Path.DirectorySeparatorChar) + 1;
            //var indexOfClassNameEnd = filePath.LastIndexOf(CSharpFileExtension);

            //if (indexOfClassNameStart != -1 && indexOfClassNameEnd != -1)
            //{
            //    className = filePath.Substring(indexOfClassNameStart, indexOfClassNameEnd - indexOfClassNameStart);
            //}

            return className;
        }
    }
}
