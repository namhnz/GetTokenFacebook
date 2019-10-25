using System.Security.Cryptography;
using System.Text;

namespace FBToken.Main.Ultis
{
    public class Md5Crypto
    {
        //https://devblogs.microsoft.com/csharpfaq/how-do-i-calculate-a-md5-hash-from-a-string/

        public static string HashCalculate(string str)
        {
            MD5 md5 = MD5.Create();
            var inputBytes = Encoding.UTF8.GetBytes(str);
            var hash = md5.ComputeHash(inputBytes);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }

            return sb.ToString().ToLower();
        }
    }
}
