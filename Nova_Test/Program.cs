using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.Intrinsics.X86;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;     // Manquant dans votre version
using Nova_Test;
using static TestClass.TestNova;


public class TestClass
{
    private readonly ILogger logger;

    public TestClass(ILogger logger)
    {
        this.logger = logger;
    }
    static async Task Main(string[] args)
    {

        bool CheckZero = await TestNova.CheckValue(0);


        var vehicles = new List<IVehicle>
        {
            new Car(),
            new Bike(),
            new Motorcycle(),
            new ElectricCar()
        };

        foreach (var v in vehicles)
        {
            Console.WriteLine($"--- {v.GetType().Name} ---");
            Console.WriteLine($"Roues: {v.Wheels}");
            v.Start();
            v.Stop();

            if (v is MotorVehicle mv)
            {
                mv.Refuel();
                if (mv is ElectricCar ec)
                {
                    ec.Charge(10);
                }
                if(v is Motorcycle mc)
                { mc.ChangemotoCycle();
                }
            }

            Console.WriteLine();
        }



        // ====== Génération de plus de 200 mesures ======
        var random = new Random();
        List<Mesure> mesures = new List<Mesure>();

        int nombreCapteurs = 10;   // 10 capteurs
        int nombreMesures = 250;   // > 200 mesures

        for (int i = 0; i < nombreMesures; i++)
        {
            mesures.Add(new Mesure
            {
                CapteurId = random.Next(1, nombreCapteurs + 1),
                Valeur = Math.Round(random.NextDouble() * 100, 2), // valeur entre 0 et 100
                Date = DateTime.Now.AddDays(-random.Next(0, 10))   // mesures sur les 10 derniers jours
            });
        }

        IDictionary<int, Stats> listGourp = mesures.Where(d => d.Date >= DateTime.Now.AddDays(-7)).GroupBy(m => m.CapteurId)
            .ToDictionary(
            g => g.Key,
            g => new Stats
            {
                Sum = g.Sum(x => x.Valeur),
                max = g.Max(x => x.Valeur),
                min = g.Min(x => x.Valeur),
                Count = g.Count()


            })
            ;

        // ====== Calcul des statistiques ======
        var stats = TestNova.CalculerStats(mesures);

        // ====== Affichage ======* 
        foreach (var kvp in stats)
        {
            Console.WriteLine($"Capteur {kvp.Key} :");
            Console.WriteLine($"  Count   = {kvp.Value.Count}");
            Console.WriteLine($"  Sum     = {kvp.Value.Sum}");
            Console.WriteLine($"  Min     = {kvp.Value.min}");
            Console.WriteLine($"  Max     = {kvp.Value.max}");
            Console.WriteLine($"  Average = {kvp.Value.Avg:F2}");
            Console.WriteLine();

        }



        //// Création d'un logger console
        //using var loggerFactory = LoggerFactory.Create(builder =>
        //{
        //    builder.AddConsole();
        //});
        //ILogger logger = loggerFactory.CreateLogger<TestClass>();

        //// Instanciation de la classe
        //var test = new TestClass(logger);

        //// Lecture du fichier produits.csv
        //var produits = await test.ReadLineProduit(
        //    @"C:\Users\poke08231\source\repos\GererFacture\produits.txt"
        //);

        //Console.WriteLine($"Produits lus : {produits.Count}");
    }

    public async Task<List<Produit>> ReadLineProduit(string path)
    {
        var produits = new List<Produit>();

        using var reader = new StreamReader(path);
        int lineNumber = 0;
        string line;

        while ((line = await reader.ReadLineAsync()) != null)
        {
            lineNumber++;
            var part = line.Split(";");


            if (part.Length != 4)
            {
                logger.LogWarning($"Ligne {lineNumber} invalide : {line}");
                continue;
            }

            try
            {
                produits.Add(new Produit
                {
                    Id = part[0],
                    Nom = part[1],
                    Quantite = int.Parse(part[2]),
                    PrixUnitaire = decimal.Parse(part[3], CultureInfo.InvariantCulture)
                });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Erreur de parsing ligne {lineNumber} : {line}");
            }
        }

        return produits;



    }


    public class TestNova
    {

        public class Mesure
        {
            public int Id;
            public double Valeur;
            public DateTime Date;
            public int CapteurId;
        }


        public class Stats
        {
            public double Sum;
            public double max = double.MinValue;
            public double min = double.MaxValue;
            public int Count;
            public double Avg => Sum / Count;

        }

        public static Dictionary<int, Stats> CalculerStats(List<Mesure> mesures)
        {
            Dictionary<int, Stats> Result = new Dictionary<int, Stats>(10000);

            DateTime DateLimite = DateTime.Now.AddDays(-7);

            foreach (Mesure mesure in mesures)
            {
                if (mesure.Date < DateLimite)
                    continue;

                if (!Result.TryGetValue(mesure.CapteurId, out var stat))
                {
                    stat = new Stats();
                    Result[mesure.CapteurId] = stat;
                }
                stat.Sum += mesure.Valeur;
                stat.Count++;
                stat.min = Math.Min(stat.min, mesure.Valeur);
                stat.max = Math.Max(stat.max, mesure.Valeur);
            }


            return Result;
        }

        public static async Task<bool> CheckValue(int x)
        {
            return await Task.Run(() =>
            {
                // traitement long
                Thread.Sleep(1500);
                return x == 0;
            });
        }

    }



 

public interface IVehicle
{
    void Start();
    void Stop();
    int Wheels { get; }
}

public abstract class MotorVehicle : IVehicle
{
    public abstract int Wheels { get; }

    public void Start()
    {
        Console.WriteLine("MotorVehicle: Démarrage du moteur...");
    }

    public void Stop()
    {
        Console.WriteLine("MotorVehicle: Arrêt du moteur.");
    }

    public abstract void Refuel();
}

public class Car : IVehicle
{
    public int Wheels => 4;
    public void Start() => Console.WriteLine("Car: Démarrage du moteur de la voiture.");
    public void Stop() => Console.WriteLine("Car: Arrêt de la voiture.");
}

public class Bike : IVehicle
{
    public int Wheels => 2;
    public void Start() => Console.WriteLine("Bike: On pousse et le vélo avance.");
    public void Stop() => Console.WriteLine("Bike: On freine et le vélo s'arrête.");
}

public class Motorcycle : MotorVehicle
{
    public override int Wheels => 2;
    public override void Refuel()
    {
        Console.WriteLine("Motorcycle: Remplissage du réservoir d'essence pour la moto.");
    }
        public void ChangemotoCycle() => Console.WriteLine($"{Wheels} roue");
}

public class ElectricCar : MotorVehicle
{
    public override int Wheels => 4;
    public int BatteryLevel { get; private set; } = 100;

    public override void Refuel()
    {
        Console.WriteLine("ElectricCar: Recharge de la batterie...");
        BatteryLevel = 100;
    }

    public void Charge(int amount)
    {
        BatteryLevel = Math.Min(100, BatteryLevel + amount);
        Console.WriteLine($"ElectricCar: Chargée de {amount}%. Niveau batterie = {BatteryLevel}%.");
    }
}


}
