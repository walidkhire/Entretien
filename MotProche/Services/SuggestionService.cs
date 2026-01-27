using MotProche.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotProche.Service
{
    public class SuggestionService : ISuggestionService
    {
        /// <summary>
        /// Retourne N suggestions de mots proches du terme donné
        /// </summary>
        public List<string> GetSuggestions(string terme, List<string> liste, int N)
        {
            terme = terme.ToLower();
            var Condidats = new List<(string mot, int DiffScore)>();
            foreach (string mot in liste)
            {
                if (terme.Length > mot.Length)
                    continue;

                int Difference = GetDifferenceScore(terme, mot.ToLower());
                Condidats.Add((mot, Difference));

            }
            //routurn liste des mots tri par différence croissante, longueur relative, ordre alphabétique
            return Condidats
                  .OrderBy(t => t.DiffScore)
                  .ThenBy(t => Math.Abs(terme.Length - t.mot.Length))
                  .ThenBy(t => t.mot)
                  .Take(N)
                  .Select(t => t.mot)
                  .ToList();
        }

        private int GetDifferenceScore(string src, string dest)
        {
            int differences = 0;
            for (int i = 0; i < src.Length; i++)
                if (src[i] != dest[i]) differences++;

            return differences;
        }
    }
}
