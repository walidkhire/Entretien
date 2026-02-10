using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace CommandeSystem.Services
{
    public class LoggerService
    {
        private readonly ConcurrentQueue<string> _logConcurrentQueue = new();
        private readonly Queue<string> _logsQueue = new();
        private readonly Channel<string> _channel = Channel.CreateUnbounded<string>();




        public LoggerService()
            {
                // Démarrage automatique du consommateur
                Task.Run(async () =>
                {
                    await foreach (var msg in _channel.Reader.ReadAllAsync())
                    {
                        Console.WriteLine(msg);
                    }
                });
            }

            public async Task LogAsync(string message)
            {
                var logEntry = $"{DateTime.Now:HH:mm:ss.fff} - {message}";
                await _channel.Writer.WriteAsync(logEntry);
            }


        public void AddLogQueue(string message)
        {
            var logEntry = $"{DateTime.Now:yyyy-MM-dd: HH:mm:ss} - {message}";
            _logsQueue.Enqueue(logEntry);
        }
        public void LogsPrintQueue()
        {
            while ((_logsQueue.Count > 0))
            {
                Console.WriteLine(_logsQueue.Dequeue());
            }

        }

        public void Log(string message)
        {
            var logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}";

            _logConcurrentQueue.Enqueue(logEntry);

        }
        public void PrintLogs()
        {
            while (_logConcurrentQueue.TryDequeue(out var logMessage))
            {
                Console.WriteLine(logMessage);
            }
        }
    }
}
