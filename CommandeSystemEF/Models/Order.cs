using System;
using System.Collections.Generic;
using System.Text;

namespace CommandeSystemEF.Models
{
    public class Order
    {
        public int Id { get; set; }  // clé primaire auto
        public int ClientId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; }  // ok
    }
}
