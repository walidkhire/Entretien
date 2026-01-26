using CommandeSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandeSystem.Services
{
    public class StockService
    {
        private readonly Dictionary<int, Product> _products = new();
        private readonly object _lock = new();

        public StockService()
        {
            _products[1] = new Product { Id = 1, Name = "PC Portable", quantityInStock = 50 };
            _products[2] = new Product { Id = 2, Name = "Clavier", quantityInStock = 100 };
        }

        public bool TryReserveStock(Order _order)
        {
            //✔ lock empêche deux threads de modifier le stock en même temps
            lock (_lock) // 🔐 synchronisation
            {
                if (_products.TryGetValue(_order.ProductId, out var product))
                {
                    if (product.quantityInStock >= _order.Quantity)
                    {
                        product.quantityInStock -= _order.Quantity;
                        return true;
                    }
                }
                return false;
            }
        }
    }
}
