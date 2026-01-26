

using CommandeSystem.Services;
using CommandeSystem.Models;
//Exécution parallèle
var stockService = new StockService();
var loggerService = new LoggerService();
var orderProcessor = new OrderProcessor(stockService, loggerService);
List<Task> tasks = new();

for (int i = 1; i <= 10; i++)
{
    int clientId = i;
    tasks.Add(Task.Run(() =>
        orderProcessor.ProcessOrderAsync(new Order 
        {

            ClientId = clientId,
            ProductId = 1,
            Quantity = 8
        })
    ));
}

await Task.WhenAll(tasks);
Console.WriteLine("Toutes les commandes ont été traitées. Voici le journal des opérations :");
//loggerService.PrintLogs();
//loggerService.PrintLogs();

Console.WriteLine("Terminer le journal des opérations :");