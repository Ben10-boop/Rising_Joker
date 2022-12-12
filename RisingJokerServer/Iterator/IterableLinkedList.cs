using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RisingJokerServer.Iterator
{
    internal class IterableLinkedList : IIterableCollection
    {
        LinkedList<long> items;

        public IterableLinkedList()
        {
            items = new LinkedList<long>();
        }

        public void Add(long item)
        {
            items.AddLast(item);
        }

        public IIterator CreateIterator()
        {
            return new LinkedListIterator(items);
        }
    }
}
