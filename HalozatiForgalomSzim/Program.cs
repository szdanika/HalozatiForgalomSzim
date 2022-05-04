using System;
using System.Collections.Generic;

namespace HalozatiForgalomSzim
{
    internal class Program
    {
        public static void kiiro(string item)
        {
            Console.WriteLine("Megerkezet az uzenet ide : " + item);
        }
        public static void Kuldo(string from, string to)
        {
            Console.WriteLine(from+" uzenetet kuld :" + to +" nak ");
        }


        public static void OnlySwitchTest()
        {
            Connections<NetworkTool> c = new Connections<NetworkTool>();
            List<NetworkTool> tesztEszkozok = new List<NetworkTool>();

            NetworkTools.Hub h1 = new NetworkTools.Hub(1,c); tesztEszkozok.Add(h1);
            NetworkTools.Hub h2 = new NetworkTools.Hub(2,c); tesztEszkozok.Add(h2);
            NetworkTools.Hub h3 = new NetworkTools.Hub(3,c); tesztEszkozok.Add(h3);
            NetworkTools.Hub h4 = new NetworkTools.Hub(4,c); tesztEszkozok.Add(h4); // cant access
            NetworkTools.Hub h5 = new NetworkTools.Hub(5, c); tesztEszkozok.Add(h5);
            NetworkTools.Hub h6 = new NetworkTools.Hub(6, c); tesztEszkozok.Add(h6);
            NetworkTools.Hub h7 = new NetworkTools.Hub(7, c); tesztEszkozok.Add(h7);

            foreach (NetworkTool eszkoz in tesztEszkozok)
            {
                c.AddTool(eszkoz);
                eszkoz.forward += Kuldo;
                eszkoz.recived += kiiro;
            }

            c.AddEdge(h1,h2);
            c.AddEdge(h1,h3);
            c.AddEdge(h2, h6);
            c.AddEdge(h3, h7);
            c.AddEdge(h5, h7);
            c.AddEdge(h5, h3);



            try { h6.Send(h6, h5, 3); }
            catch(Exceptions.ItsNotConnectedException e)
            {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine("-------------------");
            h1.ResetAll();
            h1.Send(h1, h7, 3);
            
        }
        public static void OnlyServerTest()
        {
            Connections<NetworkTool> c = new Connections<NetworkTool>();
            List<NetworkTool> tesztEszkozok = new List<NetworkTool>();

            NetworkTools.Server s1 = new NetworkTools.Server(1, c); tesztEszkozok.Add(s1);
            NetworkTools.Server s2 = new NetworkTools.Server(2, c); tesztEszkozok.Add(s2);
            NetworkTools.Server s3 = new NetworkTools.Server(3, c); tesztEszkozok.Add(s3);
            NetworkTools.Server s4 = new NetworkTools.Server(4, c); tesztEszkozok.Add(s4);
            NetworkTools.Server s5 = new NetworkTools.Server(5, c); tesztEszkozok.Add(s5);

            NetworkTools.Server s6 = new NetworkTools.Server(6, c); tesztEszkozok.Add(s6); // cant acces

            foreach (NetworkTool eszkoz in tesztEszkozok)
            {
                c.AddTool(eszkoz);
                eszkoz.forward += Kuldo;
                eszkoz.recived += kiiro;
            }

            c.AddEdge(s1, s2);
            c.AddEdge(s1, s4);
            c.AddEdge(s2, s5);
            c.AddEdge(s4, s3);
            c.AddEdge(s3, s5);



            s4.Send(s4, s5, 3);


            try { s1.Send(s1, s6, 3); }
            catch (Exceptions.ItsNotConnectedException e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public static void OnlyRouterTest()
        {
            Connections<NetworkTool> c = new Connections<NetworkTool>();
            NetworkTools.Router r1 = new NetworkTools.Router(1, c);
            NetworkTools.Router r2 = new NetworkTools.Router(2, c);
            NetworkTools.Router r3 = new NetworkTools.Router(3, c);
            NetworkTools.Router r4 = new NetworkTools.Router(4, c);
            NetworkTools.Router r5 = new NetworkTools.Router(5, c);
            NetworkTools.Router r6 = new NetworkTools.Router(6, c);

            List<NetworkTool> tesztEszkozok = new List<NetworkTool>();
            tesztEszkozok.Add(r1);
            tesztEszkozok.Add(r2);
            tesztEszkozok.Add(r3);
            tesztEszkozok.Add(r4);
            tesztEszkozok.Add(r5);
            tesztEszkozok.Add(r6);

            foreach(NetworkTool eszkoz in tesztEszkozok)
            {
                c.AddTool(eszkoz);
                eszkoz.forward += Kuldo;
                eszkoz.recived += kiiro;
            }

            c.AddEdge(r1, r2);
            c.AddEdge(r2, r3);
            c.AddEdge(r2, r4);
            c.AddEdge(r3,r5);
            c.AddEdge(r4,r5);
            c.AddEdge(r3, r6);
            c.AddEdge(r5, r6);

            r4.Send(r4, r6, 3);


        }
        public static void ServerPlusSwitch()
        {
            Connections<NetworkTool> c = new Connections<NetworkTool>();
            List<NetworkTool> tesztEszkozok = new List<NetworkTool>();

            NetworkTools.Server s1 = new NetworkTools.Server(1, c); tesztEszkozok.Add(s1);
            NetworkTools.Server s2 = new NetworkTools.Server(2, c); tesztEszkozok.Add(s2);
            NetworkTools.Server s3 = new NetworkTools.Server(3, c); tesztEszkozok.Add(s3);

            NetworkTools.Hub h1 = new NetworkTools.Hub(4, c); tesztEszkozok.Add(h1);
            NetworkTools.Hub h2 = new NetworkTools.Hub(5, c); tesztEszkozok.Add(h2);
            NetworkTools.Hub h3 = new NetworkTools.Hub(6, c); tesztEszkozok.Add(h3);

            foreach (NetworkTool eszkoz in tesztEszkozok)
            {
                c.AddTool(eszkoz);
                eszkoz.forward += Kuldo;
                eszkoz.recived += kiiro;
            }

            c.AddEdge(s1,h2);
            c.AddEdge(s1,s2);
            c.AddEdge(h1,s2);
            c.AddEdge(s2,h2);
            c.AddEdge(s2,h3);
            c.AddEdge(h2,s3);
            //c.AddEdge(s3,h3);
            //c.AddEdge(h2,h3);

            h1.Send(h1, h3, 2);
        }
        public static void AllInOne()
        {
            Connections<NetworkTool> c = new Connections<NetworkTool>();
            List<NetworkTool> tesztEszkozok = new List<NetworkTool>();

            NetworkTools.Hub h1 = new NetworkTools.Hub(1, c); tesztEszkozok.Add(h1);
            NetworkTools.Hub h2 = new NetworkTools.Hub(4, c); tesztEszkozok.Add(h2);
            NetworkTools.Hub h3 = new NetworkTools.Hub(5, c); tesztEszkozok.Add(h3);

            NetworkTools.Server s1 = new NetworkTools.Server(2, c); tesztEszkozok.Add(s1);
            NetworkTools.Server s2 = new NetworkTools.Server(6, c); tesztEszkozok.Add(s2);

            NetworkTools.Router r1 = new NetworkTools.Router(3, c); tesztEszkozok.Add(r1);
            NetworkTools.Router r2 = new NetworkTools.Router(7, c); tesztEszkozok.Add(r2);
            NetworkTools.Router r3 = new NetworkTools.Router(8, c); tesztEszkozok.Add(r3);

            foreach (NetworkTool eszkoz in tesztEszkozok)
            {
                c.AddTool(eszkoz);
                eszkoz.forward += Kuldo;
                eszkoz.recived += kiiro;
            }

            c.AddEdge(h1, s1);
            c.AddEdge(h1, r1);

            c.AddEdge(s1, h3);

            c.AddEdge(r1, h2);
            c.AddEdge(r1, s2);
            c.AddEdge(r1, r2);

            c.AddEdge(h2,h3);

            c.AddEdge(h3,s2);

            c.AddEdge(s2,r2);
            c.AddEdge(s2,r3);

            c.AddEdge(r2,r3);

            h1.Send(h1, r3, 2);
            h1.ResetAll();
        }
        static void Main(string[] args)
        {
            //OnlySwitchTest();
            //OnlyServerTest();
            //ServerPlusSwitch();
            //OnlyRouterTest();
            AllInOne();
        }
    }
}
