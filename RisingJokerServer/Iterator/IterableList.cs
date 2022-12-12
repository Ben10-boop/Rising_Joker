using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RisingJokerServer.Iterator
{
    internal class IterableList : IIterableCollection
    {
        List<long> items;
        public IterableList()
        {
            items = new List<long>();
        }

        public void Add(long item)
        {
            items.Add(item);
        }

        public IIterator CreateIterator()
        {
            return new ListIterator(items);
        }

        public IIterator CreateReverseIterator()
        {
            return new ListIteratorReverse(items);
        }
    }
}
