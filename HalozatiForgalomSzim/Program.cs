using System;

namespace HalozatiForgalomSzim
{
    internal class Program
    {
        public static void kiiro(string item)
        {
            Console.WriteLine("Megerkezet az uzenet ide : " + item);
        }
        public static void test1()
        {
            Connections<NetworkTool> c = new Connections<NetworkTool>();
            NetworkTools.Hub h1 = new NetworkTools.Hub(1,c);
            NetworkTools.Hub h2 = new NetworkTools.Hub(2,c);
            NetworkTools.Hub h3 = new NetworkTools.Hub(3,c);
            NetworkTools.Hub h4 = new NetworkTools.Hub(4,c); // cant access
            NetworkTools.Hub h5 = new NetworkTools.Hub(5, c);
            NetworkTools.Hub h6 = new NetworkTools.Hub(6, c);
            NetworkTools.Hub h7 = new NetworkTools.Hub(7, c);

            c.AddTool(h1);
            c.AddTool(h2);
            c.AddTool(h3);
            c.AddTool(h4);
            c.AddTool(h5);
            c.AddTool(h6);
            c.AddTool(h7);

            c.AddEdge(h1,h2);
            c.AddEdge(h1,h3);
            c.AddEdge(h2, h6);
            c.AddEdge(h3, h7);
            c.AddEdge(h5, h7);
            c.AddEdge(h5, h3);

            h1.recived += kiiro;
            h2.recived += kiiro;
            h3.recived += kiiro;
            h4.recived += kiiro;
            h5.recived += kiiro;
            h6.recived += kiiro;
            h7.recived += kiiro;


            try { h6.Send(h6, h5, 3); }
            catch(Exceptions.ItsNotConnectedException e)
            {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine("-------------------");
            h1.ResetAll();
            h1.Send(h1, h7, 3);
            
        }
        static void Main(string[] args)
        {
            test1();
        }
    }
}
