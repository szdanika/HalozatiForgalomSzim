using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalozatiForgalomSzim
{
    internal class Connections<T>
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
            AlreadyInList(tool);
            tools.Add(tool);
            ports.Add(new List<Edge>());
            newTool?.Invoke(tool.ToString());
        }
        public void AddEdge(T from, T to)
        {
            AlreadyHaveEdge(from ,to);

            Edge e = new Edge { To = to };

            int ind = tools.IndexOf(from);
            ports[ind].Add(e);

            e = new Edge { To = from };
            ind = tools.IndexOf(to);

            ports[ind].Add(e);

            NewEdge?.Invoke(from.ToString(), to.ToString());
        }
        public void AlreadyHaveEdge(T from, T to)
        {
            foreach (Edge e in this.Neighbors(from))
            {
                if(e.To.Equals(to))
                    throw new Exceptions.AlreadyHaveThatEdgeException();
            }
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

            while (S.Count > 0 && connected == false)
            {
                k = S.Dequeue();
                foreach (var item in Neighbors(k))
                {
                    if (item.To.Equals(to))
                        connected = true;
                    if (!F.Contains(item.To))
                    {
                        S.Enqueue(item.To);
                        F.Add(item.To);
                    }
                }
            }
            return connected;
        }
        public void AlreadyInList(T who)
        {
            if (tools.Contains(who))
                throw new Exceptions.AlreadyInListException(who.ToString());
        }
    }
}
