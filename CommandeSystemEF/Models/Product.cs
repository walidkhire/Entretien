using System;
using System.Collections.Generic;
using System.Text;

namespace CommandeSystemEF.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Stock { get; set; }
    }
}
