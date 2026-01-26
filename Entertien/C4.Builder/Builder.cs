using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Builder
{
    //Construit un objet complexe étape par étape.
    public class Pizza
    {
        public string Dough { get; set; }
        public string Sauce { get; set; }
        public string Topping { get; set; }
        public override string ToString() => $"Pizza avec {Dough} + {Sauce} + {Topping}";
    }

    // Builder
    public class PizzaBuilder
    {
        private Pizza _pizza = new Pizza();
        public PizzaBuilder SetDough(string dough) { _pizza.Dough = dough; return this; }
        public PizzaBuilder SetSauce(string sauce) { _pizza.Sauce = sauce; return this; }
        public PizzaBuilder SetTopping(string topping) { _pizza.Topping = topping; return this; }
        public Pizza Build() => _pizza;
    }

}
