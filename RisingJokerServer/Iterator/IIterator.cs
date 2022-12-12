using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RisingJokerServer.Iterator
{
    internal interface IIterator
    {
        long GetNext();
        bool HasMore();
    }
}
