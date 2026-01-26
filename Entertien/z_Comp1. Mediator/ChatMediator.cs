using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Mediator
{
    //Centralise la communication entre objets.

    public interface IChatMediator
    {
        void EnvoyerMessage(string message, Utilisateur u);
        void AjouterUtilisateur(Utilisateur u);
    }

    public class ChatMediator : IChatMediator
    {
        private List<Utilisateur> utilisateurs = new List<Utilisateur>();

        public void AjouterUtilisateur(Utilisateur u) => utilisateurs.Add(u);

        public void EnvoyerMessage(string message, Utilisateur u)
        {
            foreach (var user in utilisateurs)
                if (user != u) user.RecevoirMessage(message);
        }
    }

    public class Utilisateur
    {
        private string _nom;
        private IChatMediator _mediator;

        public Utilisateur(string nom, IChatMediator mediator)
        {
            _nom = nom; _mediator = mediator;
        }

        public void EnvoyerMessage(string message)
        {
            Console.WriteLine($"{_nom} envoie: {message}");
            _mediator.EnvoyerMessage(message, this);
        }

        public void RecevoirMessage(string message) => Console.WriteLine($"{_nom} reçoit: {message}");
    }
}
