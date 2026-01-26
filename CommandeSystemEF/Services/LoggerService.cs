using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Channels;

namespace CommandeSystemEF.Services
{
    public class LoggerService
    {
        private readonly Channel<string> _channel = Channel.CreateUnbounded<string>();

        public LoggerService()
        {
            // Démarre le consommateur en arrière-plan
            Task.Run(async () =>
            {
                await foreach (var message in _channel.Reader.ReadAllAsync())
                {
                    Console.WriteLine(message);
                }
            });
        }

        // Producteur : ajoute un log
        public async Task LogAsync(string message)
        {
            var logEntry = $"{DateTime.Now:HH:mm:ss.fff} - {message}";
            await _channel.Writer.WriteAsync(logEntry);
        }

        // Facultatif : fermer proprement
        public void Complete() => _channel.Writer.Complete();
    }
}
