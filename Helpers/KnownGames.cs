using System.Collections.Generic;

namespace Qrnick.FileServer.Helpers
{
    public static class KnownGames
    {
        public static Dictionary<string, (string, string)> GetDictionary()
        {
            var dictionary = new Dictionary<string, (string, string)>();
            dictionary.Add("dino", ("Dino", "Nie tylko do gry offline"));
            dictionary.Add("tic_tac_toe", ("Kółko i krzyżyk", "Zrelaksuj się przy klasyce"));
            return dictionary;
        }
    }
}
