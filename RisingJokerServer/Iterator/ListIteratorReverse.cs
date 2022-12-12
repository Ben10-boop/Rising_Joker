using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RisingJokerServer.Iterator
{
    internal class ListIteratorReverse : IIterator
    {
        List<long> Items;
        int CurrentIndex = 0;
        public ListIteratorReverse(List<long> items)
        {
            Items = items;
            CurrentIndex = Items.Count - 1;
        }

        public long GetNext()
        {
            long returnItem = Items[CurrentIndex];
            CurrentIndex--;
            return returnItem;
        }
        public bool HasMore()
        {
            if (CurrentIndex <= 0)
            {
                return false;
            }
            return true;
        }
    }
}
