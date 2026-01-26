using DesignPatterns.Prototype;
using DesignPatterns.Abstract_Factory;
using DesignPatterns.Builder;
using DesignPatterns.Factory;
using DesignPatterns.Mediator;
using DesignPatterns.Observer;
using DesignPatterns.Singleton;
using DesignPatterns.Strategy;
using DesignPatterns.Adapter;
using DesignPatterns.Decorator;
using DesignPatterns.Command;
using DesignPatterns.Repository;
using DesignPatterns.Facade;


namespace Entertien
{
    class Program
    {
        static void Main()
        {

            //1 Singleton
            Logger.Instance.Log("Application démarrée");
            Logger.Instance.Info("je vous informe que l'application lance correctement");


            Logger1.Instance.Log("Application démarrée");
            Logger1.Instance.Info("je vous informe que l'application lance correctement");
            //2 Factory
            Vehicule v1 = VehiculeFactory.CreerVehicule("voiture");
            v1.Conduire();
            Vehicule v2 = VehiculeFactory.CreerVehicule("moto");
            v2.Conduire();

            //3 Abstract Factory
            IGUIFactory factory = new WindowsFactory();
            var button = factory.CreateButton();
            var checkbox = factory.CreateCheckbox();
            button.Render();
            checkbox.Render();


            //4 Builder
            Pizza pizza = new PizzaBuilder()
                      .SetDough("fine")
                      .SetSauce("tomate")
                      .SetTopping("fromage")
                      .Build();
            Console.WriteLine(pizza);

            //5 Prototype

            Document doc1 = new Document { Title = "Original", Content = "Contenu original" };
            Document doc2 = doc1.Clone();
            doc2.Title = "Copie";

            Console.WriteLine(doc1.Title); // Original
            Console.WriteLine(doc2.Title); // Copie


            //----------------------------Structure---------------------------
            //1 Decorator
            IBeverage cafe = new Cafe();
            cafe = new Lait(cafe); // Ajouter Lait
            Console.WriteLine($"{cafe.GetDescription()} coûte {cafe.Cost()} €");



            //2 Adapter
            INouveauRectangle rect = new RectangleAdapter();
            rect.Dessiner(0, 0, 100, 50);



            //3 Facade
            HomeTheatreFacade facade = new HomeTheatreFacade();
            facade.RegarderFilm();






            //----------------------------Comportement---------------------------

            //1 Mediator
            IChatMediator mediator = new ChatMediator();
            var alice = new Utilisateur("Alice", mediator);
            var bob = new Utilisateur("Bob", mediator);
            var saly = new Utilisateur("Saly", mediator);

            mediator.AjouterUtilisateur(alice);
            mediator.AjouterUtilisateur(bob);
            mediator.AjouterUtilisateur(saly);

            alice.EnvoyerMessage("Salut Bob !");


            //2 Strategy
            int[] tab = { 5, 2, 8 };
            Contexte contexte = new Contexte(new TriRapide());
            contexte.TrierTableau(tab);

            //3 Command
            Lumiere lumiere = new Lumiere();
            ICommand allumer = new AllumerLumiereCommand(lumiere);
            Telecommande tele = new Telecommande();
            tele.SetCommande(allumer);
            tele.AppuyerBouton();


            //4 Repository
            IProduitRepository repo = new ProduitRepository();
            repo.Ajouter(new Produit { Id = 1, Nom = "Stylo" });
            foreach (var p in repo.GetAll())
                Console.WriteLine($"{p.Id} - {p.Nom}");

            //5 Observer
            Sujet sujet = new Sujet();
            sujet.AjouterObserver(new Observateur("Obs1"));
            sujet.AjouterObserver(new Observateur("Obs2"));

            sujet.Etat = 10;


            

        }
    }
}