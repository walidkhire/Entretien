using CommandeSystemEF.Data;
using CommandeSystemEF.Models;
using CommandeSystemEF.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

var logger = new LoggerService();

// 1️⃣ Crée la factory pour DbContext
var dbFactory = new PooledDbContextFactory<AppDbContext>(
    new DbContextOptionsBuilder<AppDbContext>()
        .UseSqlite("Data Source=orders.db")
        .Options
);

// 2️⃣ Crée le processor
var processor = new OrderProcessor(
    dbFactory,
    new ApiService(),
    new CalculationService(),
    new FileService(),
    logger
);

// 3️⃣ Exécution parallèle des commandes
List<Task> tasks = new();

for (int i = 1; i <= 10; i++)
{
    int clientId = i;
    tasks.Add(Task.Run(async () =>
        await processor.ProcessAsync(new Order
        {
            ClientId = clientId,
            ProductId = 1,
            Quantity = 5,
            CreatedAt = DateTime.Now
        })
    ));
}

await Task.WhenAll(tasks);

Console.WriteLine("\nToutes les commandes ont été traitées.");
