using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalozatiForgalomSzim.Interfaces
{
    internal interface IHalozatiEszkoz 
    {
        public int networkAddres { get; set; }
        public int bytesRecived { get; set; }
        public int bytesSent { get; set; }
        //KapcsolódóEszközök tulajdonságok
        public void Send(NetworkTool sender, NetworkTool reciver, int bytes);
        public void Recive(NetworkTool sender, NetworkTool reciver, int bytes);
    }
}
