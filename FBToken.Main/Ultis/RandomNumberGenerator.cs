using System;

namespace FBToken.Main.Ultis
{
    public class RandomNumberGenerator
    {
        public static int NumberCalculate(int min, int max)
        {
            Random rd = new Random();
            int randNum = rd.Next(min, max + 1); //Lấy cả min và cả max
            return randNum;
        }
    }
}
