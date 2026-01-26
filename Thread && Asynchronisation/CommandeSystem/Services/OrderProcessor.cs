using CommandeSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandeSystem.Services
{
    //Tasks + SemaphoreSlim
    public class OrderProcessor
    {
        private readonly StockService _stockService;
        private readonly LoggerService _loggerService;
        private readonly SemaphoreSlim _semaphore = new(5); // max 3 threads

        public OrderProcessor(StockService stockService, LoggerService loggerService)
        {
            _stockService = stockService;
            _loggerService = loggerService;
        }

        //✔ Task ✔ async/await ✔ SemaphoreSlim
        /// Processus de traitement de commande asynchrone avec limitation du nombre de threads concurrents
        public async Task ProcessOrderAsync(Order order)
        {
            await _semaphore.WaitAsync(); // ⛔ limite la concurrence
            try
            {
                await _loggerService.LogAsync(
                    $"[Thread {Thread.CurrentThread.ManagedThreadId}] " +
                    $"➡️ Début traitement | Client {order.ClientId} | Produit {order.ProductId}"
                );

                // Simulation d'un traitement long
                await Task.Delay(500);

                bool isReserved = _stockService.TryReserveStock(order);

                if (isReserved)
                {

                    await _loggerService.LogAsync(
                        $"[Thread {Thread.CurrentThread.ManagedThreadId}] " +
                        $"✅ Commande ACCEPTÉE | Client {order.ClientId} | Produit {order.ProductId}"
                    );
                }
                else
                {

                    await _loggerService.LogAsync(
                        $"[Thread {Thread.CurrentThread.ManagedThreadId}] " +
                        $"❌ Stock INSUFFISANT | Client {order.ClientId} | Produit {order.ProductId}"
                    );
                }
            }
            finally
            {

                await _loggerService.LogAsync(
                    $"[Thread {Thread.CurrentThread.ManagedThreadId}] " +
                    $"⬅️ Fin traitement | Client {order.ClientId} | Produit {order.ProductId}"
                );

                _semaphore.Release(); // ✅ libération garantie
            }
        }

    }
}
