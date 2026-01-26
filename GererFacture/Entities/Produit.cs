using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GererFacture.Entities
{
    internal class Produit : BaseEntity
    {
        public Produit(string name, string description, decimal price) : base(name)
        {
            Description = description;
            Price = price;
        }
        private string Description { get; set; }
        public decimal Price { get; set; }

    }
}
