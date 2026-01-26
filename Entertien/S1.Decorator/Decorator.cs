using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Decorator
{
    // Ajoute dynamiquement des fonctionnalités à un objet.

    // Interface
    public interface IBeverage { string GetDescription(); double Cost(); }

    // Classe concrète
    public class Cafe : IBeverage
    {
        public string GetDescription() => "Café";
        public double Cost() => 2.0;
    }

    // Décorateur abstrait
    public abstract class BeverageDecorator : IBeverage
    {
        protected IBeverage _beverage;
        public BeverageDecorator(IBeverage beverage) => _beverage = beverage;
        public abstract string GetDescription();
        public abstract double Cost();
    }

    // Décorateur concret
    public class Lait : BeverageDecorator
    {
        public Lait(IBeverage beverage) : base(beverage) { }
        public override string GetDescription() => _beverage.GetDescription() + " + Lait";
        public override double Cost() => _beverage.Cost() + 0.5;
    }

}
