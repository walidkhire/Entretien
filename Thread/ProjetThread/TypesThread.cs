using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProjetThread
{
    public class TypesThread
    {
        // ============================================================
        // Thread manuel
        // ============================================================
        public static void Travail()
        {
            Console.WriteLine($"Thread manuel - ID {Thread.CurrentThread.ManagedThreadId}");
        }

        // ============================================================
        // ThreadPool / Timer
        // ============================================================
        public static void Travail(object? state)
        {
            Console.WriteLine($"ThreadPool / Timer - ID {Thread.CurrentThread.ManagedThreadId}");
        }

        // ============================================================
        // async / await
        // ============================================================
        public static async Task TravailAsync()
        {
            await Task.Delay(2000);
            Console.WriteLine("Travail asynchrone terminé");
        }
    }
}
