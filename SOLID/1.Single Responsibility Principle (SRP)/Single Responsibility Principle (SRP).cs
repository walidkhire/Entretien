using System;

namespace Entretien.SOLID
{
    // Classe avec une seule responsabilité : contient les données du rapport
    public class Rapport
    {
        public string Contenu { get; set; }
    }

    // Classe responsable uniquement de la sauvegarde
    public class RapportSauvegarde
    {
        public void Sauvegarder(Rapport r) => Console.WriteLine("Rapport sauvegardé : " + r.Contenu);
    }

    // Classe responsable uniquement de l'impression
    public class RapportImpression
    {
        public void Imprimer(Rapport r) => Console.WriteLine("Rapport imprimé : " + r.Contenu);
    }

     
}




// Mauvais exemple : la classe gère les données ET l’impression
//public class Rapport
//{
//    public string Contenu { get; set; }

//    public void Sauvegarder()
//    {
//        Console.WriteLine("Rapport sauvegardé");
//    }

//    public void Imprimer()
//    {
//        Console.WriteLine("Rapport imprimé");
//    }
//}

//// Bon exemple : on sépare les responsabilités
//public class Rapport
//{
//    public string Contenu { get; set; }
//}

//public class RapportSauvegarde
//{
//    public void Sauvegarder(Rapport r) => Console.WriteLine("Rapport sauvegardé");
//}

//public class RapportImpression
//{
//    public void Imprimer(Rapport r) => Console.WriteLine("Rapport imprimé");
//}
