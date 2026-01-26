using GererFacture.Entities;
using GererFacture.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GererFacture.Service
{
    public class FactureService : IFactureService
    {
        decimal IFactureService.Calculer(List<Facture> Facture)
        {
            return Facture.Sum(F => F.Total);
        }
    }
}
