using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalozatiForgalomSzim.NetworkTools
{
    internal class Server : NetworkTool
    {//Csak olyanra amelyfele el lehet erni a celpontot
        public override event Recived recived;
        public override event Forwarder forward;
        public Server(int address, Connections<NetworkTool> connection) : base(address, connection)
        {
        }

        public override void Recive(NetworkTool sender, NetworkTool reciver, int bytes)
        {
            if(reciver == this)
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

            foreach (var item in WhereCanISend(this, reciver))
            {
                if(!sentMassage.Contains(item))
                {
                    forward?.Invoke(this.ToString(), item.ToString());
                    sentMassage.Add(item);
                    item.ListAdd(this);
                    item.Recive(sender, reciver, bytes);
                }
            }
        }

        public List<NetworkTool> WhereCanISend(NetworkTool sender, NetworkTool reciver)
        {
            List<NetworkTool> list = new List<NetworkTool>();
            foreach(var item in connections.Neighbors(sender))
            {
                List<NetworkTool> seen = new List<NetworkTool>();
                seen.Add(this);
                if(IsItConnected(ref seen, reciver, item.To))
                    list.Add(item.To);
            }
            return list;
        }
        public bool IsItConnected(ref List<NetworkTool> seen, NetworkTool end, NetworkTool corrent)
        {
            bool levelseen = false;

            if (corrent == end)
                return true;

            foreach(var item in connections.Neighbors(corrent))
            {
                if(!seen.Contains(item.To))
                {
                    seen.Add(item.To);
                    if(IsItConnected(ref seen, end, item.To))
                        levelseen = true;
                }
            }
            return levelseen;
        }
    }
}
