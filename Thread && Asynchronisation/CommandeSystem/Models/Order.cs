using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandeSystem.Models
{
    public class Order
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int ClientId { get; set; }
    }
}
