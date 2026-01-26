//1.Qu’est - ce que Dispose ?

//Dispose() est une méthode de l’interface IDisposable.

//Son rôle : libérer les ressources non managées (fichiers, connexions réseau ou base de données, handles Windows, etc.).

//En C#, le ramasse-miettes (GC) gère la mémoire managée automatiquement, mais les ressources non managées doivent être libérées explicitement.

using System;
using System.IO;

class simple
{
    static void Main()
    {
        // Sans using : il faut appeler Dispose() explicitement
        FileStream fs = new FileStream("test.txt", FileMode.Create);
        fs.WriteByte(0x0A);
        fs.Dispose(); // libération manuelle des ressources

        // Avec using : Dispose() est appelé automatiquement
        using (FileStream fs2 = new FileStream("test2.txt", FileMode.Create))
        {
            fs2.WriteByte(0x0B);
        } // fs2.Dispose() est appelé ici automatiquement
    }
}

//✅ Avantages du using :
//Code plus sûr.
//Pas de risque d’oublier d’appeler Dispose().
//Les ressources sont libérées même en cas d’exception.

