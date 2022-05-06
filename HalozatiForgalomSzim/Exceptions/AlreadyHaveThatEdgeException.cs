using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalozatiForgalomSzim.Exceptions
{
    internal class AlreadyHaveThatEdgeException : Exception
    {
        public AlreadyHaveThatEdgeException() : base("That tool already has that edge")
        { }
    }
}
