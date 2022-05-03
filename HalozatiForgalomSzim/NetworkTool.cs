using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalozatiForgalomSzim
{
    public delegate void NewEdgeHasBeenCreated(string from, string to);
    public delegate void NewTool(string name);
    public delegate void Recived(string who);
    public delegate void Forwarder(string from,string to);
    abstract internal class NetworkTool : Interfaces.IHalozatiEszkoz
    {
        public abstract event Forwarder forward;
        public abstract event Recived recived;

        protected Connections<NetworkTool> connections;
        public  int networkAddres { get; set; }
        public  int bytesRecived { get; set; }
        public  int bytesSent { get; set; }
        protected List<NetworkTool> sentMassage = new List<NetworkTool>();


        public abstract void Recive(NetworkTool sender, NetworkTool reciver, int bytes);
        public abstract void Send(NetworkTool sender, NetworkTool reciver, int bytes);
        public override string ToString()
        {
            return networkAddres.ToString();
        }
        public void ListAdd(NetworkTool WhatToAdd)
        {
            sentMassage.Add(WhatToAdd);
        }
        public NetworkTool(int address, Connections<NetworkTool> connection)
        {
            this.networkAddres = address;
            this.connections = connection;
        }
        public void ResetAll()
        {
            foreach(var item in connections.tools)
            {
                item.sentMassage.Clear();
            }
        }
    }
}
