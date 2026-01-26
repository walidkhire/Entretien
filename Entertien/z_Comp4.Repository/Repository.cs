using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Repository
{
    //  Sépare la logique de stockage de l’accès aux données, pratique avec Entity Framework ou tout CRUD.

    // Entité
    public class Produit { public int Id; public string Nom; }

    // Repository
    public interface IProduitRepository
    {
        void Ajouter(Produit p);
        Produit GetById(int id);
        IEnumerable<Produit> GetAll();
    }

    // Implémentation simple en mémoire
    public class ProduitRepository : IProduitRepository
    {
        private List<Produit> produits = new List<Produit>();

        public void Ajouter(Produit p) => produits.Add(p);
        public Produit GetById(int id) => produits.Find(p => p.Id == id);
        public IEnumerable<Produit> GetAll() => produits;
    }
}
