using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuggestionMot.Core.Services
{
    /// <summary>
    /// Interface pour le service de suggestions de mots
    /// </summary>
    public interface ISuggestionService
    {
        /// <summary>
        /// Retourne N suggestions de mots proches du terme donné
        /// </summary>
        /// <param name="terme">Terme recherché</param>
        /// <param name="liste">Liste de mots candidats</param>
        /// <param name="N">Nombre maximum de suggestions</param>
        /// <returns>Liste de suggestions triées par similarité, longueur et ordre alphabétique</returns>
        List<string> GetSuggestions(string terme, List<string> liste, int N);
    }
}
