using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Adapter
{
    // Permet à des classes incompatibles de travailler ensemble.

    // Classe existante (ancienne API)
    public class AncienRectangle
    {
        public void Dessiner(int x, int y, int w, int h) => Console.WriteLine($"Ancien rectangle {x},{y},{w},{h}");
    }

    // Nouvelle interface
    public interface INouveauRectangle { void Dessiner(int x1, int y1, int x2, int y2); }

    // Adapter
    public class RectangleAdapter : INouveauRectangle
    {
        private AncienRectangle _ancienRectangle = new AncienRectangle();
        public void Dessiner(int x1, int y1, int x2, int y2)
        {
            int w = x2 - x1;
            int h = y2 - y1;
            _ancienRectangle.Dessiner(x1, y1, w, h);
        }
    }
}
