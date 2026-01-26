using CommandeSystemEF.Data;
using CommandeSystemEF.Models;
using CommandeSystemEF.Services;
using Microsoft.EntityFrameworkCore;

public class OrderProcessor
{
    private readonly IDbContextFactory<AppDbContext> _dbFactory;
    private readonly LoggerService _logger;
    private readonly ApiService _api;
    private readonly CalculationService _calc;
    private readonly FileService _file;
    private readonly SemaphoreSlim _semaphore = new(5);

    public OrderProcessor(
        IDbContextFactory<AppDbContext> dbFactory,
        ApiService api,
        CalculationService calc,
        FileService file,
        LoggerService logger
    )
    {
        _dbFactory = dbFactory;
        _api = api;
        _calc = calc;
        _file = file;
        _logger = logger;
    }

    public async Task ProcessAsync(Order order)
    {
        await _semaphore.WaitAsync();
        try
        {
            await _logger.LogAsync($"➡️ Début commande Client {order.ClientId}");

            var apiResult = await _api.CallExternalApiAsync();
            var calcResult = await _calc.HeavyCalculationAsync();

            // ❌ PAS de DbContext partagé
            await using var db = _dbFactory.CreateDbContext();
            db.Orders.Add(order);
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine("Erreur EF Core : " + ex.Message);
                if (ex.InnerException != null)
                    Console.WriteLine("Inner : " + ex.InnerException.Message);
            }

            await _file.WriteLogToFileAsync($"Commande {order.ClientId} traitée");
            await _logger.LogAsync($"✅ Commande OK | API={apiResult} | Calc={calcResult:F2}");
        }
        finally
        {
            _semaphore.Release();
        }
    }
}
