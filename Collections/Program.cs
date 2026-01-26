using Collections.Application;
using Collections.Domain;
using Collections.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

// -----------------------------
// Création du container DI
// -----------------------------
var services = new ServiceCollection();

// Configuration du logging pour la console
services.AddLogging(config => config.AddConsole());

// Enregistrement des services et repositories
services.AddSingleton<IMonnaieRepository, MonnaieRepository>();
services.AddSingleton<IMonnaieService, MonnaieService>();

// Construction du provider DI
var provider = services.BuildServiceProvider();

// -----------------------------
// Récupération du service via DI
// -----------------------------
var monnaieService = provider.GetRequiredService<IMonnaieService>();
var logger = provider.GetRequiredService<ILogger<Program>>();

try
{
    // -----------------------------
    // Ajout de monnaies
    // -----------------------------
    var euro = new Monnaie
    {
        Id = Guid.NewGuid(),
        Code = "EUR",
        Name = "Euro",
        Valeur = 1,
        Description = "Euro (zone Schengen)"
    };
    var dollar = new Monnaie
    {
        Id = Guid.NewGuid(),
        Code = "USD",
        Name = "Dollar",
        Valeur = 1,
        Description = "Dollar américain"
    };
    var dinar = new Monnaie
    {
        Id = Guid.NewGuid(),
        Code = "DZ",
        Name = "Dinar",
        Valeur = 1,
        Description = "Dinar algérien"
    };

    monnaieService.Add(euro);
    monnaieService.Add(dollar);
    monnaieService.Add(dinar);

    logger.LogInformation("Monnaies ajoutées avec succès.");

    // -----------------------------
    // Affichage des monnaies
    // -----------------------------
    DisplayMonnaies(monnaieService.GetAll());

    // -----------------------------
    // Mise à jour d'une monnaie (valeur + description)
    // -----------------------------
    monnaieService.Update(euro.Id, new Dictionary<string, object>
    {
        ["Valeur"] = 100,
        ["Description"] = "Euro mis à jour (zone Schengen)"
    });
    logger.LogInformation("Mise à jour partielle de l'Euro effectuée.");

    // -----------------------------
    // Suppression d'une monnaie
    // -----------------------------
    monnaieService.Remove(dollar.Id);
    logger.LogInformation("Suppression du Dollar effectuée.");

    // -----------------------------
    // Affichage après mise à jour et suppression
    // -----------------------------
    Console.WriteLine("\nAprès mise à jour et suppression :");
    DisplayMonnaies(monnaieService.GetAll());

    

    // -----------------------------
    // Mise à jour d'une monnaie (valeur + description)
    // -----------------------------
    monnaieService.Update(dollar.Id, new Dictionary<string, object>
    {
        ["Valeur"] = 500,
        ["Code"]="USD1",
        ["Description"] = "Dollar américain  mis à jour"
    });
    logger.LogInformation("Mise à jour partielle de l'Euro effectuée.");

}
catch (Exception ex)
{
    logger.LogError(ex, "Une erreur est survenue lors de l'exécution.");
}

Console.WriteLine("\nFin de l'exécution. Appuyez sur une touche pour quitter...");
Console.ReadKey();

// -----------------------------
// Méthode utilitaire pour afficher les monnaies
// -----------------------------
void DisplayMonnaies(System.Collections.ObjectModel.ObservableCollection<Monnaie> monnaies)
{
    Console.WriteLine($"\nNombre de monnaies : {monnaies.Count}");
    foreach (var m in monnaies)
    {
        Console.WriteLine($"{m.Code} - {m.Name} - Valeur : {m.Valeur} - Desc: {m.Description}");
    }
}
