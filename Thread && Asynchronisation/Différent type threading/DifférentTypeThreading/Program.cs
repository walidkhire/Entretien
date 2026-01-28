
using System.Linq;
using System.Threading.Tasks.Parallel;
using System.Threading.Tasks.Task;
using System.Threading.Thread;
using System.Threading.ThreadPool;
using System.Threading.Timer;//base ThreadPool
using System.Timers.Timer;//service/serveur


/*Thread simple
1 .Thread natif / bas niveau
Crée un vrai thread OS


- Gestion manuelle (start, join, priorité, etc.)
- Coûteux en ressources
- Rarement recommandé aujourd’hui

📌 À utiliser seulement pour des besoins très spécifiques*/

Thread thread = new Thread(() =>
{
    // Code à exécuter dans le thread
    Console.WriteLine("Thread simple en cours d'exécution.");
});
thread.Start();
thread.Join();//attend la fin du thread
Console.WriteLine("Thread simple terminé.");



/*🔹 2.ThreadPool(System.Threading.ThreadPool)
👉 Pool de threads géré par .NET

-Threads réutilisés
-Plus performant que Thread
-Pas de contrôle direct (priorité, arrêt, etc.)

📌 Base de nombreux mécanismes modernes*/

ThreadPool threadPool = new ThreadPool();
threadPool.queueuseworkitem(() =>
{
    // Code à exécuter dans le thread pool
    Console.WriteLine("ThreadPool en cours d'exécution.");
});

// Pas de join, le thread pool gère la durée de vie des threads

/*🔹 3.Task(System.Threading.Tasks.Task)

👉 Abstraction moderne du threading

-S’appuie sur le ThreadPool
-Gestion automatique
-Support des exceptions, annulation, continuation



📌 Méthode recommandée dans 95 % des cas*/

Task task = Task.Run(() =>
{
    // Code à exécuter dans la tâche
    Console.WriteLine("Task en cours d'exécution.");
});
task.Wait();//attend la fin de la tâche
Console.WriteLine("Task terminée.");

/*🔹 4.Task<T>(System.Threading.Tasks.Task<T>)
 
👉 Tâche qui retourne une valeur
-Similaire à Task
-Supporte les types de retour
📌 Utile pour les opérations asynchrones qui produisent un résultat*/
Task<int> taskWithResult = Task.Run(() =>
{
    // Code à exécuter dans la tâche
    Console.WriteLine("Task<T> en cours d'exécution.");
    return 42;
});
int result = taskWithResult.Result;//attend et récupère le résultat
Console.WriteLine($"Task<T> terminée avec le résultat : {result}.");

/*🔹5. Async/Await (System.Threading.Tasks)
 Ne crée pas toujours un thread


-Idéal pour I/O (HTTP, fichiers, DB, etc.)
-Très performant et lisible

📌 Standard moderne en .NET
*/
async Task LoadAsync()
{
    await Task.Delay(1000);
}
// Simule une opération asynchrone
await LoadAsync();
Console.WriteLine("Opération asynchrone terminée.");

/*🔹 6. Parallel (System.Threading.Tasks.Parallel)
 *👉 Parallélisme de données
 
-Exécution parallèle automatique
-S’appuie sur le ThreadPool

📌 Utile pour calculs CPU intensifs*/


Parallel.For(0, 10, i => { /* code */ });
Console.WriteLine("Parallel.For terminé.");

/* 🔹 7. plinq (System.Linq)
 * 👉 LINQ en parallèle
📌 Facile mais attention aux performances réelles*/

var result = list.AsParallel().Where(x => x > 10).ToList();

/*🔹 8. Timer (System.Threading.Timer vs System.Timers.Timer)
 *👉 Exécution périodique
 *a) System.Threading.Timer
ThreadPool*/

Timer timer = new Timer(_ =>
        {
            Console.WriteLine($"Tick ThreadPool : {DateTime.Now}");
        },
        null,
        0,      // délai initial (ms)
        1000);  // période (ms)

Console.ReadLine(); // empêche la fermeture
/*b) System.Timers.Timer
Serveur / services*/

var timer = new Timer(1000); // 1 seconde
timer.Elapsed += (sender, e) =>
{
    Console.WriteLine($"Tick serveur : {DateTime.Now}");
};

timer.AutoReset = true;
timer.Start();

Console.ReadLine();
        }


/*c) System.Windows.Forms.Timer
UI (WinForms)*/

     Timer timer = new Timer();

public Form1()
{
    InitializeComponent();

    timer.Interval = 1000; // 1 seconde
    timer.Tick += (s, e) =>
    {
        label1.Text = DateTime.Now.ToString("HH:mm:ss");
    };
    timer.Start();
}


/*d) DispatcherTimer
UI (WPF)*/
DispatcherTimer timer = new DispatcherTimer();

public MainWindow()
{
    InitializeComponent();

    timer.Interval = TimeSpan.FromSeconds(1);
    timer.Tick += (s, e) =>
    {
        MyLabel.Content = DateTime.Now.ToString("HH:mm:ss");
    };
    timer.Start();
}




/*🔹 10.ValueTask
👉 Optimisation avancée de Task

-Réduit les allocations mémoire
-Cas très spécifiques

📌 À utiliser uniquement si nécessaire*/
class Service
{
    private int? _cache;

    public ValueTask<int> GetValueAsync()
    {
        // Cas rapide : pas d'allocation de Task
        if (_cache.HasValue)
        {
            return new ValueTask<int>(_cache.Value);
        }

        // Cas lent : allocation d'un vrai Task
        return new ValueTask<int>(LoadAsync());
    }

    private async Task<int> LoadAsync()
    {
        await Task.Delay(1000); // simulation I/O
        _cache = 42;
        return _cache.Value;
    }
}

//utilistion
var service = new Service();

int value1 = await service.GetValueAsync(); // lent
int value2 = await service.GetValueAsync(); // rapide (cache)


/*🔹 11.SynchronizationContext / Dispatcher
👉 Contexte de thread (UI / ASP.NET)
WinForms / WPF / ASP.NET


Gestion du thread principal*/


Dispatcher.Invoke(() => { /* UI */ });


public async Task<IActionResult> Index()
{
    // Thread ASP.NET
    var threadBefore = Thread.CurrentThread.ManagedThreadId;

    await Task.Delay(1000); // I/O async

    // Thread après await (retour au SynchronizationContext ASP.NET)
    var threadAfter = Thread.CurrentThread.ManagedThreadId;

    return Content($"Before: {threadBefore}, After: {threadAfter}");
}

//2️⃣ WPF (Dispatcher)

public MainWindow()
{
    InitializeComponent();

    Task.Run(() =>
    {
        string message = $"Background Thread: {Thread.CurrentThread.ManagedThreadId}";

        Dispatcher.Invoke(() =>
        {
            MyLabel.Content = $"UI Thread: {Thread.CurrentThread.ManagedThreadId}\n{message}";
        });
    });
}

//WinForms (Control.Invoke)
public Form1()
{
    InitializeComponent();

    Task.Run(() =>
    {
        // Thread en arrière-plan
        string message = $"Thread: {Thread.CurrentThread.ManagedThreadId}";

        // Remonter sur le thread UI
        this.Invoke(() =>
        {
            label1.Text = $"UI Thread: {Thread.CurrentThread.ManagedThreadId}\n{message}";
        });
    });
}
