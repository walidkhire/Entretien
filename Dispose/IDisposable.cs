using System;

public class Ressource : IDisposable
{
    private bool disposed = false;

    public void Utiliser()
    {
        if (disposed) throw new ObjectDisposedException("Ressource");
        Console.WriteLine("Ressource utilisée");
    }

    public void Dispose()
    {
        if (!disposed)
        {
            Console.WriteLine("Libération des ressources");
            disposed = true;
            GC.SuppressFinalize(this); // empêche le finalizer si Dispose déjà appelé
        }
    }

    ~Ressource()
    {
        Dispose(); // Finalizer en dernier recours
    }
}

// Utilisation
class Program
{
    static void Main()
    {
        using (var r = new Ressource())
        {
            r.Utiliser();
        } // Dispose est appelé automatiquement ici
    }
}

//🌟 4. Bonnes pratiques pour Dispose

//Toujours implémenter IDisposable si la classe utilise des ressources non managées.

//Utiliser using pour que Dispose soit appelé automatiquement.

//Éviter de laisser des ressources ouvertes (fichiers, connections, streams).

//Appeler GC.SuppressFinalize(this) dans Dispose() si vous utilisez un finalizer.

//Vérifier l’état avec un bool disposed pour éviter les appels multiples.
