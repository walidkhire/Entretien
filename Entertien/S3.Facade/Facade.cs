using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Facade
{
    //Simplifie l’accès à un sous-système complexe en offrant une interface unique.

    // Sous-système complexe
    public class SystemeAudio { public void Allumer() => Console.WriteLine("Audio On"); }
    public class SystemeVideo { public void Allumer() => Console.WriteLine("Vidéo On"); }

    // Facade
    public class HomeTheatreFacade
    {
        private SystemeAudio audio = new SystemeAudio();
        private SystemeVideo video = new SystemeVideo();

        public void RegarderFilm()
        {
            Console.WriteLine("Préparation du Home Cinema...");
            audio.Allumer();
            video.Allumer();
        }
    }

}
