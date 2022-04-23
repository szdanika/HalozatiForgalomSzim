using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalozatiForgalomSzim.Exceptions
{
    internal class ItsNotConnectedException : Exception
    {
        public ItsNotConnectedException(NetworkTool from, NetworkTool to) : base(from.ToString() + " is not connected to " + to.ToString())
        {
            
        }
    }
}
