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
    abstract internal class NetworkTool : Interfaces.IHalozatiEszkoz
    {
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
    public class Connections<T>
    {
        public event NewEdgeHasBeenCreated NewEdge;
        public event NewTool newTool;

        public class Edge
        {
            public T To { get; set; }
        }

        public List<T> tools = new List<T>();
        public List<List<Edge>> ports = new List<List<Edge>>();

        public void AddTool(T tool)
        {
            tools.Add(tool);
            ports.Add(new List<Edge>());
            newTool?.Invoke(tool.ToString());
        }
        public void AddEdge(T from, T to)
        {
            Edge e = new Edge { To = to };

            int ind = tools.IndexOf(from);
            ports[ind].Add(e);

            e = new Edge { To = from };
            ind = tools.IndexOf(to);

            ports[ind].Add(e);

            NewEdge?.Invoke(from.ToString(), to.ToString());
        }
        public List<Edge> Neighbors(T tool)
        {
            return ports[tools.IndexOf(tool)];
        }
        public bool ItsConnected(T from, T to)
        {
            Queue<T> S = new Queue<T>();
            S.Enqueue(from);
            List<T> F = new List<T>();
            F.Add(from);
            bool connected = false;
            T k;

            while(S.Count > 0 && connected == false)
            {
                k = S.Dequeue();
                foreach(var item in Neighbors(k))
                {
                    if(item.To.Equals(to))
                        connected = true;
                    if(!F.Contains(item.To))
                    {
                        S.Enqueue(item.To);
                        F.Add(item.To);
                    }
                }
            }
            return connected;
        }
    }
}
