using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalozatiForgalomSzim.Exceptions
{
    internal class AlreadyInListException : Exception
    {
        public AlreadyInListException(string eszkoznev) : base(eszkoznev.ToString() + " is already in the list")
        {

        }
    }
}
