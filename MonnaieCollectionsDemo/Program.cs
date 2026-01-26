// -----------------------------
// 1️⃣ List<Monnaie>
// -----------------------------
using Collections.Domain;

class Program
{
    static void Main()
    {
        List<Monnaie> listMonnaie = new List<Monnaie>()
                    {
                        new Monnaie(){Id=Guid.NewGuid(),Code="DZ",Name="Dinar",Description="Dinar Algérien",Valeur=1},
                        new Monnaie(){Id=Guid.NewGuid(),Code="EUR",Name="Euro",Description="EUR Shéngan",Valeur=1},
                        new Monnaie(){Id=Guid.NewGuid(),Code="USD",Name="Dollar",Description="Dollar Américan",Valeur=1},
                        new Monnaie(){Id=Guid.NewGuid(),Code="DRH",Name="DRHAM",Description="DARHAM Maroccain",Valeur=1}
                    };
        listMonnaie.ForEach(e => Console.WriteLine($"{e.Code} -{e.Name}  - Valeur:{e.Valeur}"));
        listMonnaie.OrderBy(e => e.Code);
        listMonnaie.ForEach(e => Console.WriteLine($"{e.Name}  {e.Description}  valeur ${e.Valeur}"));

    }
}