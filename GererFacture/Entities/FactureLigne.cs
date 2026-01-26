using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GererFacture.Entities
{
    internal class FactureLigne
    {
        public FactureLigne()
        {
        }
        private Guid Id { get; set; }
        private Produit produit { get; set; }
        private int qty { get; set; }
        public decimal Total => produit.Price * qty;

    }
}
