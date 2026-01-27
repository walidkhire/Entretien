using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotProche
{
    public interface ISuggestionService
    {
        List<string> GetSuggestions(string terme, List<string> liste, int N);
    }
}
