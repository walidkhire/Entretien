using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Factory
{
    //Crée des objets sans exposer la logique de construction.
    public abstract class Vehicule
    {
        public abstract void Conduire();
    }

    public class Voiture : Vehicule
    {
        public override void Conduire() => Console.WriteLine("Conduire une voiture 🚗");
    }

    public class Moto : Vehicule
    {
        public override void Conduire() => Console.WriteLine("Conduire une moto 🏍️");
    }

    // Factory
    public static class VehiculeFactory
    {
        public static Vehicule CreerVehicule(string type) => type switch
        {
            "voiture" => new Voiture(),
            "moto" => new Moto(),
            _ => throw new ArgumentException("Type inconnu")
        };
    }
}
