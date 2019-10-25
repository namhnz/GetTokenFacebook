using System.Collections.Generic;
using System.Linq;

namespace FBToken.Main.Ultis
{
    public class QueryString
    {
        public static string Stringify(Dictionary<string, string> parameters)
        {
            //https://stackoverflow.com/questions/23518966/convert-a-dictionary-to-string-of-url-parameters

            return string.Join('&', parameters.Select(kvp => $"{kvp.Key}={kvp.Value}"));
        }
    }
}
