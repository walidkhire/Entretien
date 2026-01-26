using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID.Dependency_Inversion_Principle
{
    //Les modules de haut niveau ne doivent pas dépendre des modules de bas niveau, mais des abstractions.
    //Les détails doivent dépendre des abstractions, pas l’inverse.
    // Mauvais : classe dépend directement d’une implémentation
    public class ServiceAlerte
    {
        private EmailService _email = new EmailService();
        public void Envoyer(string message) => _email.EnvoyerEmail(message);
    }

    public class EmailService
    {
        public void EnvoyerEmail(string msg) => Console.WriteLine($"Email envoyé: {msg}");
    }

    // Bon : dépend d’une abstraction
    public interface IAlerte { void Envoyer(string message); }

    public class EmailAlerte : IAlerte
    {
        public void Envoyer(string message) => Console.WriteLine($"Email envoyé: {message}");
    }

    public class ServiceAlerteV2
    {
        private IAlerte _alerte;
        public ServiceAlerteV2(IAlerte alerte) => _alerte = alerte;
        public void Envoyer(string message) => _alerte.Envoyer(message);
    }

    // Utilisation
    class Program
    {
        static void Main()
        {
            IAlerte alerte = new EmailAlerte();
            var service = new ServiceAlerteV2(alerte);
            service.Envoyer("Bonjour !");
        }
    }


}
