using System;

namespace SOLID.InterfaceSegregationPrinciple
{
    // Interfaces segmentées selon ISP
    public interface IImprimante
    {
        void Imprimer();
    }

    public interface IScanner
    {
        void Scanner();
    }

    // Imprimante simple : n’implémente que ce dont elle a besoin
    public class ImprimanteSimple : IImprimante
    {
        public void Imprimer() => Console.WriteLine("Impression OK");
    }

    // Imprimante multifonction : implémente les deux interfaces
    public class MultiFonction : IImprimante, IScanner
    {
        public void Imprimer() => Console.WriteLine("Impression OK");
        public void Scanner() => Console.WriteLine("Scan OK");
    }

   
}



//// Mauvais : une interface trop large
//public interface IImprimante
//{
//    void Imprimer();
//    void Scanner();
//}

//// Problème : une imprimante simple n’a pas de scanner

//// Bon : on segmente les interfaces
//public interface IImprimante { void Imprimer(); }
//public interface IScanner { void Scanner(); }

//public class ImprimanteSimple : IImprimante { public void Imprimer() => Console.WriteLine("Impression OK"); }
//public class MultiFonction : IImprimante, IScanner
//{
//    public void Imprimer() => Console.WriteLine("Impression OK");
//    public void Scanner() => Console.WriteLine("Scan OK");
//}

