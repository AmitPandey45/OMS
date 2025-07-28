using Core.Framework.Logging;
using Microsoft.Extensions.Logging;
using System.IO.Compression;

namespace Core.Infrastructure.Logging.Microsoft
{
    public class JsonRollingFileLoggerProvider : ILoggerProvider
    {
        private readonly string _logDirectory;
        private readonly long _maxFileSize;
        private readonly int _retentionDays;
        private readonly int _maxArchiveCount;
        private readonly ILogContext _logContext;

        public JsonRollingFileLoggerProvider(string logDirectory, long maxFileSize, int retentionDays, int maxArchiveCount, ILogContext logContext)
        {
            _logDirectory = logDirectory;
            _maxFileSize = maxFileSize;
            _retentionDays = retentionDays;
            _maxArchiveCount = maxArchiveCount;
            _logContext = logContext;

            if (!Directory.Exists(_logDirectory))
            {
                Directory.CreateDirectory(_logDirectory);
            }

            CleanupOldLogFiles();
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new JsonRollingFileLogger(categoryName, _logDirectory, _maxFileSize, _retentionDays, _maxArchiveCount, _logContext);
        }

        public void Dispose() { }

        private void CleanupOldLogFiles()
        {
            var logFiles = new DirectoryInfo(_logDirectory).GetFiles("*.log");

            foreach (var file in logFiles)
            {
                if (file.CreationTimeUtc.AddDays(_retentionDays) < DateTime.UtcNow)
                {
                    try
                    {
                        file.Delete();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error deleting old log file {file.FullName}: {ex.Message}");
                    }
                }
            }

            ArchiveOldLogFiles();
        }

        private void ArchiveOldLogFiles()
        {
            var logFiles = new DirectoryInfo(_logDirectory).GetFiles("*.log");
            var archiveDir = System.IO.Path.Combine(_logDirectory, "Archive");
            if (!Directory.Exists(archiveDir))
            {
                Directory.CreateDirectory(archiveDir);
            }

            foreach (var file in logFiles)
            {
                if (file.CreationTimeUtc.AddDays(_retentionDays) < DateTime.UtcNow)
                {
                    string archiveFileName = System.IO.Path.Combine(archiveDir, $"{file.Name}.{file.CreationTimeUtc:yyyyMMddHHmmss}.zip");
                    try
                    {
                        ZipFile.CreateFromDirectory(file.DirectoryName, archiveFileName);
                        file.Delete();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error Archiving file {file.FullName}: {ex.Message}");
                    }
                }
            }

            LimitArchiveFiles(archiveDir);
        }

        private void LimitArchiveFiles(string archiveDir)
        {
            var archiveFiles = new DirectoryInfo(archiveDir).GetFiles("*.zip");
            if (archiveFiles.Length > _maxArchiveCount)
            {
                var sortedFiles = archiveFiles.OrderBy(f => f.CreationTimeUtc).ToArray();
                for (int i = 0; i < archiveFiles.Length - _maxArchiveCount; i++)
                {
                    try
                    {
                        sortedFiles[i].Delete();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error deleting archive file {sortedFiles[i].FullName}: {ex.Message}");
                    }
                }
            }
        }
    }
}
