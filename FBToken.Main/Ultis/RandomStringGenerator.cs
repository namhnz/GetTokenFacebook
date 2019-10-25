using System;

namespace FBToken.Main.Ultis
{
    public class RandomStringGenerator
    {
        public static string StringCalculate(int limit)
        {
            if (limit <= 0)
            {
                limit = 10;
            }

            Random rd = new Random();

            var text = "abcdefghijklmnopqrstuvwxyz";
            var firstText = text[rd.Next(text.Length)];
            var possible = "abcdefghijklmnopqrstuvwxyz0123456789";

            string randomText = firstText.ToString();

            for (int i = 0; i < limit - 1; i++)
            {
                randomText += possible[rd.Next(possible.Length)].ToString();
            }

            return randomText;
        }
    }
}
