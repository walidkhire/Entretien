using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID.Open_Closed_Principle
{
    //Principe : Une classe doit être ouverte à l’extension mais fermée à la modification.
    
    
    // Mauvais : on modifie la classe pour ajouter un nouveau type
    public class CalculateurPrix
    {
        public double Calculer(string typeProduit, double prix)
        {
            if (typeProduit == "A") return prix * 0.9;
            if (typeProduit == "B") return prix * 0.8;
            return prix;
        }
    }

    // Bon : on utilise l’héritage ou stratégie pour étendre
    public abstract class PrixStrategy
    {
        public abstract double Calculer(double prix);
    }

    public class PrixProduitA : PrixStrategy { public override double Calculer(double prix) => prix * 0.9; }
    public class PrixProduitB : PrixStrategy { public override double Calculer(double prix) => prix * 0.8; }

    public class Calculateur
    {
        public double Calculer(PrixStrategy strategy, double prix) => strategy.Calculer(prix);
    }
}


