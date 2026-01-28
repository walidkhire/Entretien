using ProjetThread;
using System;
using System.Data;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        // ============================================================
        // 1️⃣ Thread (thread manuel – bas niveau)
        // ============================================================
        /*
         DÉFINITION :
         - Crée un vrai thread système (OS Thread)
         - Gestion manuelle du cycle de vie du thread

         UTILISATION :
         - Cas très spécifiques bas niveau
         - Quand on veut un contrôle total sur le thread

         AVANTAGES :
         + Contrôle total
         + Priorité, état, gestion fine

         INCONVÉNIENTS :
         - Coûteux en ressources
         - Complexe
         - Peu scalable
         - Rarement utilisé en prod moderne
        */

        Thread thread = new Thread(new ThreadStart(TypesThread.Travail));
        thread.Start();

        Thread.Sleep(500);


        // ============================================================
        // 2️⃣ ThreadPool (pool de threads)
        // ============================================================
        /*
         DÉFINITION :
         - Pool de threads géré par .NET
         - Threads réutilisés automatiquement

         UTILISATION :
         - Petites tâches rapides
         - Traitement parallèle simple

         AVANTAGES :
         + Très performant
         + Léger
         + Gestion automatique

         INCONVÉNIENTS :
         - Aucun contrôle sur les threads
         - Pas de priorité
         - Debug plus difficile
        */

        ThreadPool.QueueUserWorkItem(TypesThread.Travail);

        Thread.Sleep(500);


        // ============================================================
        // 3️⃣ Task (abstraction moderne)
        // ============================================================
        /*
         DÉFINITION :
         - Abstraction au-dessus du ThreadPool
         - Modèle moderne de parallélisme

         UTILISATION :
         - Cas général
         - Calculs
         - Traitements asynchrones

         AVANTAGES :
         + Simple
         + Lisible
         + Recommandé
         + Compatible async/await

         INCONVÉNIENTS :
         - Moins de contrôle bas niveau
         - Debug parfois complexe
        */

        await Task.Run(() =>
        {
            Console.WriteLine("Travail avec Task");
        });


        // ============================================================
        // 4️⃣ async / await (asynchrone moderne)
        // ============================================================
        /*
         DÉFINITION :
         - Modèle asynchrone non bloquant
         - Ne crée pas forcément de thread

         UTILISATION :
         - API HTTP
         - Base de données
         - Fichiers
         - I/O

         AVANTAGES :
         + Scalabilité
         + Performance
         + Lisibilité
         + Non bloquant

         INCONVÉNIENTS :
         - Complexité mentale pour débutants
         - Mauvaise gestion peut créer deadlocks
        */

        await TypesThread.TravailAsync();


        // ============================================================
        // 5️⃣ Parallel (parallélisme CPU)
        // ============================================================
        /*
         DÉFINITION :
         - Parallélisme multi-cœurs
         - Exécution concurrente CPU

         UTILISATION :
         - Calculs mathématiques
         - Traitement d’images
         - Big data
         - Algorithmes

         AVANTAGES :
         + Exploite tous les cœurs CPU
         + Très rapide
         + Automatique

         INCONVÉNIENTS :
         - Race conditions possibles
         - Synchronisation nécessaire
         - Pas adapté I/O 
        📥 Input (Entrée) : le programme reçoit des données
        📤 Output (Sortie) : le programme envoie des données
        */

        Parallel.For(0, 5, i =>
        {
            Console.WriteLine($"Iteration {i} - Thread {Thread.CurrentThread.ManagedThreadId}");
        });


        // ============================================================
        // 6️⃣ Timer (tâches planifiées)
        // ============================================================
        /*
         DÉFINITION :
         - Exécute une action à intervalle régulier
         - Basé sur le ThreadPool

         UTILISATION :
         - Tâches répétitives
         - Polling ==>sondage
         - Vérifications périodiques
         - Jobs simples

         AVANTAGES :
         + Simple
         + Léger
         + Automatique

         INCONVÉNIENTS :
         - Problèmes de concurrence
         - Pas de gestion métier
         - Pas fiable pour tâches critiques
        */

        Timer timer = new Timer(TypesThread.Travail, null, 0, 1000);
        await Task.Delay(3000);
        timer.Dispose();

        Console.WriteLine("\nFIN DU PROGRAMME");
    }
}



/* 🔥 C’est quoi un problème de concurrence ?

👉 Un problème de concurrence apparaît quand plusieurs threads/tâches accèdent en même temps à la même donnée partagée
➡️ et que le résultat dépend de l’ordre d’exécution(imprévisible).

💣 Résultat :

valeurs fausses

bugs aléatoires

crashs

bugs impossibles à reproduire

🧠 Exemple très simple(compteur)
int compteur = 0;

    Parallel.For(0, 1000, i =>
{
    compteur++;
});

Console.WriteLine(compteur);


❓ Résultat attendu : 1000
❌ Résultat réel : souvent moins(ex : 732, 891…)

➡️ PROBLÈME DE CONCURRENCE

❌ Pourquoi ça arrive ?

L’instruction :

compteur++;


N’est PAS atomique ❌
Elle fait en réalité :

lire compteur

ajouter 1

réécrire compteur

Si deux threads le font en même temps 👉 collision

🧨 Types principaux de problèmes de concurrence
1️⃣ Race condition (le plus courant)

Qui arrive en premier gagne 🏁

if (solde >= 100)
{
    solde -= 100;
}


Deux threads passent le if → solde négatif ❌

2️⃣ Deadlock (blocage mutuel)
lock (A)
{
    lock (B)
    {
    }
}


Et ailleurs :

lock (B)
{
    lock (A)
    {
    }
}


👉 Les deux threads s’attendent à vie 💀

3️⃣ Starvation (famine)

👉 Un thread n’obtient jamais les ressources

4️⃣ Lost update (mise à jour perdue)

👉 Une écriture écrase l’autre

✅ Comment éviter les problèmes de concurrence
🔐 1️⃣ lock (le plus simple)
object locker = new object();

lock (locker)
{
    compteur++;
}


✔️ Un seul thread à la fois
❌ Peut ralentir si mal utilisé

🚦 2️⃣ SemaphoreSlim (limiter l’accès)
SemaphoreSlim semaphore = new SemaphoreSlim(2);

await semaphore.WaitAsync();
try
{
    Console.WriteLine("Accès autorisé");
}
finally
{
    semaphore.Release();
}

⚡ 3️⃣ Opérations atomiques(Interlocked)
Interlocked.Increment(ref compteur);


✔️ Ultra rapide
✔️ Très sûr
❌ Limité à des cas simples

📦 4️⃣ Collections thread-safe
ConcurrentDictionary<int, string> dict = new();
dict.TryAdd(1, "A");

🧠 5️⃣ Règle d’or (pro)

Moins d’état partagé = moins de bugs

✔️ Immuabilité
✔️ Variables locales
✔️ Messages au lieu de partage

⚠️ async/await n’élimine PAS la concurrence
await Task.Delay(1000);


👉 Peut reprendre sur un autre thread
👉 Les problèmes existent toujours

🧪 Exemple réel (API Web)

❌ Mauvais :

static int total = 0;

total++;


➡️ Plusieurs requêtes = bug

✅ Bon :

Interlocked.Increment(ref total);

🧠 Résumé ultra clair
Problème	Cause
Race condition	Accès simultané
Deadlock	Locks mal ordonnés
Lost update	Écriture concurrente
Starvation	Ressource monopolisée
🎯 À retenir

💥 Concurrence = puissance + danger
🧠 Protection = obligatoire
🚀 Bon code = simple + prévisible
*/