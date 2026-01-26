using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova_Test
{
    public class Repository : IRepository
    {
        private readonly string _connectionString;

        List<Commande> commandes;
        public Repository(string ConnectionString)
        {
            _connectionString = ConnectionString;
        }

        public void InsertFacture(string Id, decimal montant)
        {
            //  using var Connection = new SqlConnection(string _connectionString);
            var commende= commandes.Where(c => c.DateCommande >= DateTime.Now.AddDays(-30)).GroupBy(c => c.IdClient).
                Select(g => new
                {
                    IdClient = g.Key,
                    CA = g.Sum(e => e.Montant)
                })
                .OrderByDescending(e => e.CA).ToList();
            ;
        }


    }
    public class Commande
    {
        public string IdClient { get; set; } = ""; // Identifiant du client
        public DateTime DateCommande { get; set; } // Date de la commande
        public decimal Montant { get; set; }       // Montant total de la commande


      

       
    }


   
}


 