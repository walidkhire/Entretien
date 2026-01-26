using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GererFacture.Entities
{
    internal class Facture
    {
        public Guid Id { get; set; }
        public Client Client { get; set; }
        public List<FactureLigne> Facts { get; set; }
        public decimal Total => Facts.Sum(p => p.Total);
    }
}
