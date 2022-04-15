using System;

namespace HalozatiForgalomSzim
{
    internal class Program
    {
        public static void kiiro(string item)
        {
            Console.WriteLine("Hozza adtam a " + item);
        }
        public static void test1()
        {
            NetworkTools.Router<string> router = new NetworkTools.Router<string>(2);

            Console.WriteLine(router);
        }
        static void Main(string[] args)
        {
            test1();
        }
    }
}
