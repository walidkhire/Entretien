using System;

namespace ConceptsCSharp
{
    // 🌟 1️⃣ sealed
    // Empêche une classe d’être héritée.
    // Peut aussi empêcher une méthode override d’être redéfinie dans une classe dérivée.
    public sealed class Voiture
    {
        public void Demarrer() => Console.WriteLine("Voiture démarrée");
    }

    // 🌟 2️⃣ static
    // Indique que la classe ou le membre appartient à la classe elle-même, pas à une instance.
    // Les classes static ne peuvent pas être instanciées.
    public static class MathUtils
    {
        public static int Addition(int a, int b) => a + b;
    }

    // 🌟 3️⃣ const et readonly
    // const : Valeur connue à la compilation, ne peut pas changer
    // readonly : Valeur assignée au moment de l’initialisation ou dans le constructeur, ne peut plus changer après
    public class Exemple
    {
        public const double Pi = 3.1415;
        public readonly int Id;
        public Exemple(int id) { Id = id; }
    }

    // 🌟 4️⃣ partial
    // Permet de séparer une classe, une struct ou une interface en plusieurs fichiers.
    // Pratique pour organiser du code généré automatiquement ou volumineux.
    public partial class Personne
    {
        public string Nom { get; set; }
    }

    public partial class Personne
    {
        public string Prenom { get; set; }
    }

    // 🌟 5️⃣ readonly struct
    // Rend une struct immuable, ses champs ne peuvent pas être modifiés après l’initialisation.
    // Bon pour les types value sécurisés et performants.
    public readonly struct Point
    {
        public int X { get; }
        public int Y { get; }
        public Point(int x, int y) { X = x; Y = y; }
    }

    // 🌟 6️⃣ event et delegate
    // Permettent de mettre en place des notifications et le pattern Observer.
    // delegate = type qui référence une méthode.
    // event = encapsule un delegate pour les abonnés.
    public delegate void MonEventHandler(string message);

    public class Publisher
    {
        public event MonEventHandler SurEvenement;
        public void Declencher() => SurEvenement?.Invoke("Evenement déclenché");
    }

    // 🌟 7️⃣ ref et out
    // ref : passe une variable par référence, doit être initialisée avant l’appel.
    // out : passe une variable par référence, doit être assignée dans la méthode.
    public class RefOutExample
    {
        public void ModifierRef(ref int x) { x += 5; }
        public void ModifierOut(out int x) { x = 10; }
    }

    // 🌟 8️⃣ params
    // Permet de passer un nombre variable d’arguments à une méthode.
    public class ParamsExample
    {
        public int Somme(params int[] nombres)
        {
            int total = 0;
            foreach (var n in nombres) total += n;
            return total;
        }
    }

    // 🌟 9️⃣ nullable et ?? / ?.
    // ? après un type = autoriser valeur null.
    // ?. = accès sûr aux membres, retourne null si objet null.
    // ?? = valeur par défaut si null.
    // Exemple intégré dans Main()

    // 🌟 🔟 lock
    // Sert à synchroniser l’accès aux ressources partagées dans un environnement multithread.
    public class ThreadSafeCounter
    {
        private static object verrous = new object();
        private static int compteur = 0;

        public void Incrementer()
        {
            lock (verrous)
            {
                compteur++;
                Console.WriteLine($"Compteur = {compteur}");
            }
        }
    }

    // 🌟 Programme principal pour tester tous les concepts
    class Program
    {
        static void Main()
        {
            // Sealed
            Voiture v = new Voiture();
            v.Demarrer();

            // Static
            int resultat = MathUtils.Addition(3, 5);
            Console.WriteLine("Addition = " + resultat);

            // Const et readonly
            Exemple e = new Exemple(42);
            Console.WriteLine($"Pi = {Exemple.Pi}, Id = {e.Id}");

            // Partial
            Personne p = new Personne { Nom = "Dupont", Prenom = "Jean" };
            Console.WriteLine($"Nom: {p.Nom}, Prenom: {p.Prenom}");

            // Readonly struct
            Point point = new Point(2, 3);
            Console.WriteLine($"Point: ({point.X}, {point.Y})");

            // Event et delegate
            Publisher pub = new Publisher();
            pub.SurEvenement += msg => Console.WriteLine(msg);
            pub.Declencher();

            // Ref et out
            RefOutExample ro = new RefOutExample();
            int a = 2;
            ro.ModifierRef(ref a);
            Console.WriteLine("Ref a = " + a);
            int b;
            ro.ModifierOut(out b);
            Console.WriteLine("Out b = " + b);

            // Params
            ParamsExample pe = new ParamsExample();
            int somme = pe.Somme(1, 2, 3, 4);
            Console.WriteLine("Somme = " + somme);

            // Nullable et opérateurs ?? / ?.
            int? nullableInt = null;
            int val = nullableInt ?? 5;
            string s = null;
            Console.WriteLine($"Nullable int = {val}, longueur string = {s?.Length}");

            // Lock
            ThreadSafeCounter counter = new ThreadSafeCounter();
            counter.Incrementer();
            counter.Incrementer();
        }
    }
}
