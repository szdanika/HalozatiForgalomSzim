﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalozatiForgalomSzim.NetworkTools
{
    internal class Router : NetworkTool //legjobb utvonalra kuldi tovabb
    {
        public Router(int addres, Connections<NetworkTool> con) : base(addres, con)
        {
        }

        public override void Recive(NetworkTool sender, NetworkTool reciver, int bytes)
        {
            throw new NotImplementedException();
        }

        public override void Send(NetworkTool sender, NetworkTool reciver, int bytes)
        {
           
        }
    }
}
