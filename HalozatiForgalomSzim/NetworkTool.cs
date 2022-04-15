using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalozatiForgalomSzim
{
    public delegate void NewEdgeHasBeenCreated(string from, string to);
    public delegate void NewTool(string name);
    abstract internal class NetworkTool<T> : Interfaces.IHalozatiEszkoz<T>
    {
        public  int networkAddres { get; set; }
        public  int bytesRecived { get; set; }
        public  int bytesSent { get; set; }

        public abstract void Recive(T sender, T reciver, int bytes);
        public abstract void Send(T sender, T reciver, int bytes);
        class Connections
        {
            public event NewEdgeHasBeenCreated NewEdge;
            public event NewTool newTool;

            protected class Edge
            {
                public T To { get; set; }
            }
            List<T> tools = new List<T>();
            List<List<Edge>> ports = new List<List<Edge>>();

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
        }
        public override string ToString()
        {
            return networkAddres.ToString();
        }
        public NetworkTool(int addres)
        {
            this.networkAddres = addres;
        }

    }
}
