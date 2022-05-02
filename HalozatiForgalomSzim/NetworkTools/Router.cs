using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalozatiForgalomSzim.NetworkTools
{
    internal class Router : NetworkTool //legjobb utvonalra kuldi tovabb
    {//legjobb kezdot megnezni
        public event Recived recived;

        public Router(int addres, Connections<NetworkTool> con) : base(addres, con)
        {
        }

        public override void Recive(NetworkTool sender, NetworkTool reciver, int bytes)
        {
            if (reciver == this)
                recived?.Invoke(this.ToString());
            else
                this.Send(sender, reciver, bytes);
        }

        public override void Send(NetworkTool sender, NetworkTool reciver, int bytes)
        {
            if (sender == this)
            {
                if (!connections.ItsConnected(sender, reciver))
                    throw new Exceptions.ItsNotConnectedException(sender, reciver);
            }

            NetworkTool sendtowho = BestRouteFind(this, reciver);
            Console.WriteLine(this.ToString() + " kuldi : " + sendtowho.ToString());
            sendtowho.Recive(sender, reciver, bytes);

        }
        public NetworkTool BestRouteFind(NetworkTool sender, NetworkTool reciver)
        {
            //s que f halmaz
            Queue<NetworkTool> s = new Queue<NetworkTool>();
            List<NetworkTool> f = new List<NetworkTool>();
            int[] dist = new int[connections.tools.Count];
            NetworkTool[] from = new NetworkTool[connections.tools.Count];
            
            dist[connections.tools.IndexOf(sender)] = 0;
            s.Enqueue(sender);
            f.Add(sender);
            NetworkTool k;


            while(s.Count > 0)
            {
                k = s.Dequeue();
                foreach(var item in connections.Neighbors(k))
                {
                    if(!f.Contains(item.To))
                    {
                        s.Enqueue(item.To);
                        f.Add(item.To);
                        int index = connections.tools.IndexOf(item.To); // kovetkezo

                        if(dist[index] > dist[connections.tools.IndexOf(k)]+1 || dist[index] == 0)
                        {
                            dist[index] = dist[connections.tools.IndexOf(k)] + 1;
                            from[index] = k;
                        }    
                        //dist[connections.tools.IndexOf(item.To)] = dist[connections.tools.IndexOf(k)] + 1;
                    }
                }
            }
            //Console.WriteLine("legjobb ut erre vezett :" + BackTrackFind(from, reciver, sender).ToString());
                return BackTrackFind(from, reciver, sender);
        }
        public NetworkTool BackTrackFind(NetworkTool[] list, NetworkTool corrent, NetworkTool start)
        {
            bool gotit = false;
            NetworkTool eppenezo = corrent;
            while(gotit == false)
            {
                if (list[connections.tools.IndexOf(eppenezo)] == start)
                    gotit = true;
                else
                    eppenezo = list[connections.tools.IndexOf(eppenezo)];
            }
            return eppenezo;
        }
        
        
    }
}
