using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace DesignPatterns.Strategy
{
    // Permet de changer dynamiquement le comportement d’un objet.

    public interface ITri
    {
        void Trier(int[] tableau);
    }

    public class TriBulles : ITri
    {
        public void Trier(int[] tableau)
        {
            Array.Sort(tableau); // simple pour l’exemple
            Console.WriteLine("Tri Bulles utilisé");
        }
    }

  
    public class Contexte
    {
        private ITri _strategie;
        public Contexte(ITri strategie) => _strategie = strategie;
        public void TrierTableau(int[] tableau) => _strategie.Trier(tableau);
    }

}
