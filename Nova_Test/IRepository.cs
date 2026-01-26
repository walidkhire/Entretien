using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova_Test
{
    public interface IRepository
    {
        public void InsertFacture(string Id, decimal montant);
    }
}
