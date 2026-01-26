// Classes séparées selon la responsabilité
using Entretien.SOLID;
using SOLID.Dependency_Inversion_Principle;
using SOLID.InterfaceSegregationPrinciple;
using SOLID.Liskov_Substitution_Principle;
using SOLID.Open_Closed_Principle;
using System.Text;


//1.Single Responsibility Principle(SRP)
// Création du rapport
Rapport rapport = new Rapport { Contenu = "Rapport mensuel" };

// Sauvegarde
RapportSauvegarde sauvegarde = new RapportSauvegarde();
sauvegarde.Sauvegarder(rapport);

// Impression
RapportImpression impression = new RapportImpression();
impression.Imprimer(rapport);





//2️. Open_Closed Principle (OCP)
Calculateur calc = new Calculateur();
PrixStrategy strategyA = new PrixProduitA();
PrixStrategy strategyB = new PrixProduitB();

// Utilisation
Console.WriteLine(calc.Calculer(strategyA, 100)); // 90
Console.WriteLine(calc.Calculer(strategyB, 100)); // 80


//3-Liskov Substitution Principle (LSP)
List<IForme> formes = new List<IForme>();
formes.Add(new RectangleSolide { Largeur = 5, Hauteur = 10 });
formes.Add(new CarreSolide { Cote = 7 });

// Utilisation
foreach (var f in formes)
{
    Console.WriteLine($"Aire: {f.Aire()}");
}


//4.Interface Segregation Principle (ISP)
IImprimante imprimante = new ImprimanteSimple();
imprimante.Imprimer();

MultiFonction mf = new MultiFonction();
mf.Imprimer();
mf.Scanner();



//5.Dependency Inversion Principle (DIP)
IAlerte alerte = new EmailAlerte();
ServiceAlerteV2 service = new ServiceAlerteV2(alerte);

// Utilisation
service.Envoyer("Bonjour !"); // Utilise l'abstraction IAlerte






//✅ Les 5 principes SOLID

//Les principes SOLID sont des bonnes pratiques de conception orientée objet, popularisés par Robert C. Martin (Uncle Bob).
//Ils améliorent :

//la maintenabilité

//l’évolutivité

//la testabilité

//la flexibilité

//la lisibilité du code

//1️⃣ S — Single Responsibility Principle (SRP)
//Principe :

//Une classe doit avoir une seule raison de changer.
//Autrement dit : une classe = une seule responsabilité / un seul rôle.

//Pourquoi ?

//Le code devient plus simple

//Les bugs sont plus faciles à isoler

//Les modifications n’ont pas d’effets de bord

//Exemple simple :

//❌ Une classe qui gère la logique métier et l’écriture dans un fichier.
//✔ Séparer en deux classes : ReportGenerator et FileWriter.

//2️⃣ O — Open/Closed Principle (OCP)
//Principe :

//Les classes doivent être ouvertes à l’extension mais fermées à la modification.

//Pourquoi ?

//On peut ajouter de nouvelles fonctionnalités sans toucher au code existant, donc moins de bugs.

//Exemple :

//Au lieu de modifier une classe pour gérer un nouveau type de paiement,
//✔ On crée une nouvelle classe qui implémente une interface IPayment.

//3️⃣ L — Liskov Substitution Principle (LSP)
//Principe :

//Toute classe dérivée doit pouvoir remplacer sa classe mère sans créer d’erreurs.

//Pourquoi ?

//Garantit un héritage correct

//Évite les surprises et comportements incohérents

//Exemple :

//Une classe Rectangle et une classe dérivée Square (mauvais héritage).
//Car un carré casse le comportement d’un rectangle (largeur ≠ hauteur).

//4️⃣ I — Interface Segregation Principle (ISP)
//Principe :

//Mieux vaut plusieurs petites interfaces spécialisées qu'une interface large et générale.

//Pourquoi ?

//Évite que des classes implémentent des méthodes inutiles

//Facilite la réutilisation

//Code plus propre et cohérent

//Exemple :

//❌ Une interface IPrinter qui force à implémenter Print(), Scan(), Fax()
//✔ La séparer en IPrintable, IScannable, IFaxable

//5️⃣ D — Dependency Inversion Principle (DIP)
//Principe :

//Les modules de haut niveau ne doivent pas dépendre de modules de bas niveau.
//Les deux doivent dépendre d’abstractions (interfaces).

//Pourquoi ?

//Réduit les dépendances fortes

//Facilite les tests (injection de dépendance, mocks)

//Permet de changer l’implémentation sans casser le code

//Exemple :

//❌ Une classe OrderService dépend directement de SqlDatabase.
//✔ Elle dépend d’une interface IDatabase pour pouvoir changer la base facilement.

//🎯 Résumé en une ligne chacun
//Principe	Résumé
//S – SRP	Une classe doit faire une seule chose.
//O – OCP	On étend, on ne modifie pas.
//L – LSP	Une classe dérivée doit pouvoir remplacer la base.
//I – ISP	Plusieurs petites interfaces valent mieux qu’une grosse.
//D – DIP	Dépendre d’abstractions, pas de classes concrètes.