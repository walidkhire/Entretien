using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;


namespace DesignPatterns.Observer
{
    //Notifier automatiquement les objets quand l’état d’un autre change.


public class Sujet
    {
        private List<IObserver> observers = new List<IObserver>();
        private int _etat;
        public int Etat
        {
            get => _etat;
            set { _etat = value; Notifier(); }
        }

        public void AjouterObserver(IObserver observer) => observers.Add(observer);

        private void Notifier()
        {
            foreach (var obs in observers) obs.MettreAJour(_etat);
        }
    }

    public interface IObserver { void MettreAJour(int etat); }

    public class Observateur : IObserver
    {
        private string _nom;
        public Observateur(string nom) => _nom = nom;
        public void MettreAJour(int etat) => Console.WriteLine($"{_nom} a reçu le nouvel état: {etat}");
    }
}