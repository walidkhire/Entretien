using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Singleton
{
    // Garantit qu’il n’existe qu’une seule instance d’une classe.

    public class Logger
    {
        private static Logger _instance;
        private static readonly object _lock = new object();

        private Logger() { }

        public static Logger Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                        _instance = new Logger();
                    return _instance;
                }
            }
        }

        public void Log(string message)
        {
            Console.WriteLine($"Log: {message}");
        }
        public void Info(string message)
        {
            Console.Write($"Info : {message}");
        }


    }

    public class Logger1
    {
        private static readonly Lazy<Logger1> _instance =
            new Lazy<Logger1>(() => new Logger1());

        private Logger1() { }

        public static Logger1 Instance => _instance.Value;

        public void Log(string message)
        {
            Console.WriteLine($"Log: {message}");
        }
        public void Info(string message)
        {
            Console.Write($"Info : {message}");
        }
    }
}
