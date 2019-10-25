using System.Collections.Generic;
using System.Linq;

namespace FBToken.Main.Ultis
{
    public class SortDictionary
    {
        public static Dictionary<string, string> Sort(Dictionary<string, string> obj)
        {
            var sortedKeys = obj.Keys.OrderBy(key => key);
            var sortedObj = new Dictionary<string, string>();
            foreach (var sortedKey in sortedKeys)
            {
                sortedObj.Add(sortedKey, obj[sortedKey]);
            }

            return sortedObj;
        }
    }
}
