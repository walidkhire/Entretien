using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace DesignPatterns.Command
{
    //Encapsule une action sous forme d’objet.Très utilisé pour Undo/Redo ou boutons d’interface.

    // Commande
    public interface ICommand { void Execute(); }

    // Commande concrète
    public class AllumerLumiereCommand : ICommand
    {
        private Lumiere _lumiere;
        public AllumerLumiereCommand(Lumiere lumiere) => _lumiere = lumiere;
        public void Execute() => _lumiere.Allumer();
    }

    // Récepteur
    public class Lumiere
    {
        public void Allumer() => Console.WriteLine("La lumière est allumée !");
    }

    // Invoker
    public class Telecommande
    {
        private ICommand _commande;
        public void SetCommande(ICommand commande) => _commande = commande;
        public void AppuyerBouton() => _commande.Execute();
    }
}
