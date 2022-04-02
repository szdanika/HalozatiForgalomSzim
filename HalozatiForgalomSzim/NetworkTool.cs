using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalozatiForgalomSzim
{
    abstract internal class NetworkTool : Interfaces.IHalozatiEszkoz
    {
        public  int networkAddres { get; set; }
        public  int bytesRecived { get; set; }
        public  int bytesSent { get; set; }

        public abstract void Recive(string sender, string reciver, int bytes);
        public abstract void Send(string sender, string reciver, int bytes);
    }
}
