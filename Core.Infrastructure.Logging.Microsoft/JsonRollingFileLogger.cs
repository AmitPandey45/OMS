using Core.Framework.Logging;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;

namespace Core.Infrastructure.Logging.Microsoft
{
    public class JsonRollingFileLogger : ILogger
    {
        private readonly string _categoryName;
        private readonly string _logDirectory;
        private readonly long _maxFileSize;
        private readonly int _retentionDays;
        private readonly int _maxArchiveCount;
        private string _currentLogFile;
        private readonly ILogContext _logContext;

        public JsonRollingFileLogger(string categoryName, string logDirectory, long maxFileSize, int retentionDays, int maxArchiveCount, ILogContext logContext)
        {
            _categoryName = categoryName;
            _logDirectory = logDirectory;
            _maxFileSize = maxFileSize;
            _retentionDays = retentionDays;
            _maxArchiveCount = maxArchiveCount;
            _currentLogFile = GetCurrentLogFileName();
            _logContext = logContext;
        }

        public IDisposable BeginScope<TState>(TState state) => null!;

        public bool IsEnabled(LogLevel logLevel) => true;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            var message = formatter(state, exception);

            var logEntry = new LogEntry
            {
                TimeStamp = DateTime.UtcNow.ToString("o"),
                LogLevel = logLevel.ToString(),
                LogInfo = state,
                UniqueRequestId = _logContext?.UniqueRequestId,
                ThreadId = Thread.CurrentThread.ManagedThreadId,
                TenantId = "Test Tenant",
                AccountId = "Test Account",
                UserId = _logContext?.UserId,
                Api = _logContext?.Api,
                HttpMethod = _logContext?.HttpMethod,
                HostName = Dns.GetHostName(),
                HttpStatusCode = _logContext?.HttpStatusCode,
                LogMessage = message,
                Request = _logContext.Request,
                Response = _logContext.Response,
                UserAgent = _logContext?.UserAgent,
                IpAddress = _logContext?.IpAddress,
            };

            // Set the JsonWriterOptions to make it indented
            var jsonOptions = new JsonSerializerOptions
            {
                WriteIndented = true // Enables indentation for the JSON output
            };

            var jsonLogEntry = JsonSerializer.Serialize(logEntry, jsonOptions) + Environment.NewLine;

            lock (this)
            {
                if (File.Exists(_currentLogFile) && new FileInfo(_currentLogFile).Length > _maxFileSize)
                {
                    RollLogFile();
                }

                System.IO.File.AppendAllText(_currentLogFile, jsonLogEntry);
            }
        }

        private string GetCurrentLogFileName()
        {
            return System.IO.Path.Combine(_logDirectory, $"{DateTime.UtcNow:yyyyMMdd}.log");
        }

        private void RollLogFile()
        {
            var newLogFile = GetCurrentLogFileName();
            _currentLogFile = newLogFile;
        }
    }
}
