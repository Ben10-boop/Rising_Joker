using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RisingJokerServer.Iterator
{
    internal interface IIterableCollection
    {
        IIterator CreateIterator();
        void Add(long item);
    }
}
