using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace   SOLID.Liskov_Substitution_Principle
{
    //Principe : Les objets d’une classe dérivée doivent pouvoir remplacer la classe de base sans casser le programme.
    
    
    // Mauvais : un carré hérite de rectangle mais change le comportement
    public class Rectangle
    {
        public virtual int Largeur { get; set; }
        public virtual int Hauteur { get; set; }
        public int Aire() => Largeur * Hauteur;
    }

    public class Carre : Rectangle
    {
        public override int Largeur { set { base.Largeur = base.Hauteur = value; } }
        public override int Hauteur { set { base.Hauteur = base.Largeur = value; } }
    }

    // Bon : éviter d’hériter si cela casse le comportement
    public interface IForme { int Aire(); }
    public class RectangleSolide : IForme { public int Largeur, Hauteur; public int Aire() => Largeur * Hauteur; }
    public class CarreSolide : IForme { public int Cote; public int Aire() => Cote * Cote; }

}
