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
        public static void OnlyServerTest()
        {
            Connections<NetworkTool> c = new Connections<NetworkTool>();
            NetworkTools.Server s1 = new NetworkTools.Server(1, c);
            NetworkTools.Server s2 = new NetworkTools.Server(2, c);
            NetworkTools.Server s3 = new NetworkTools.Server(3, c);
            NetworkTools.Server s4 = new NetworkTools.Server(4, c);
            NetworkTools.Server s5 = new NetworkTools.Server(5, c);

            NetworkTools.Server s6 = new NetworkTools.Server(6, c); // cant acces

            c.AddTool(s1);
            c.AddTool(s2);
            c.AddTool(s3);
            c.AddTool(s4);
            c.AddTool(s5);
            c.AddTool(s6);

            c.AddEdge(s1, s2);
            c.AddEdge(s1, s4);
            c.AddEdge(s2, s5);
            c.AddEdge(s4, s3);
            c.AddEdge(s3, s5);

            s1.recived += kiiro;
            s2.recived += kiiro;
            s3.recived += kiiro;
            s4.recived += kiiro;
            s5.recived += kiiro;
            s6.recived += kiiro;


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

            NetworkTools.Server s1 = new NetworkTools.Server(1, c);
            NetworkTools.Server s2 = new NetworkTools.Server(2, c);
            NetworkTools.Server s3 = new NetworkTools.Server(3, c);

            NetworkTools.Hub h1 = new NetworkTools.Hub(4, c);
            NetworkTools.Hub h2 = new NetworkTools.Hub(5, c);
            NetworkTools.Hub h3 = new NetworkTools.Hub(6, c);

            c.AddTool(s1);
            c.AddTool(s2);
            c.AddTool(s3);

            c.AddTool(h1);
            c.AddTool(h2);
            c.AddTool(h3);

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

        }
        static void Main(string[] args)
        {
            //OnlySwitchTest();
            //OnlyServerTest();
            //ServerPlusSwitch();

            AllInOne();
        }
    }
}
