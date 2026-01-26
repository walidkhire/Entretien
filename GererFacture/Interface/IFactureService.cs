using GererFacture.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GererFacture.Interface
{
    internal interface IFactureService
    {
        public decimal Calculer(List<Facture> Facture);
    }
}
