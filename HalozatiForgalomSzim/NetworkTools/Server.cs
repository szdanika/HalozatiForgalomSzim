using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalozatiForgalomSzim.NetworkTools
{
    internal class Server : NetworkTool
    {//Csak olyanra amelyfele el lehet erni a celpontot
        public Server(int address, Connections<NetworkTool> connection) : base(address, connection)
        {
        }

        public override void Recive(NetworkTool sender, NetworkTool reciver, int bytes)
        {
            throw new NotImplementedException();
        }

        public override void Send(NetworkTool sender, NetworkTool reciver, int bytes)
        {
            throw new NotImplementedException();
        }
    }
}
