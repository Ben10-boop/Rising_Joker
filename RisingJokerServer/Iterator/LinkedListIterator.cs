using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RisingJokerServer.Iterator
{
    internal class LinkedListIterator : IIterator
    {
        LinkedList<long> Items;
        List<long> ListItems;
        int CurrentIndex = 0;
        public LinkedListIterator(LinkedList<long> items)
        {
            Items = items;
            ListItems = items.ToList();
        }

        public long GetNext()
        {
            long returnItem = ListItems[CurrentIndex];
            CurrentIndex++;
            return returnItem;
        }
        public bool HasMore()
        {
            if (CurrentIndex + 1 >= Items.Count)
            {
                return false;
            }
            return true;
        }
    }
}
