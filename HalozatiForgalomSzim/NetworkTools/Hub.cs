using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalozatiForgalomSzim.NetworkTools
{
    internal class Hub<T> : NetworkTool<T> where T : class
    {
        public Hub(int addres) : base(addres)
        {
        }

        public override void Recive(T sender, T reciver, int bytes)
        {
            throw new NotImplementedException();
        }

        public override void Send(T sender, T reciver, int bytes)
        {
            throw new NotImplementedException();
        }
    }
}
