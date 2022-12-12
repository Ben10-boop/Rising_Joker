using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RisingJokerServer.Iterator
{
    internal class ListIterator : IIterator
    {
        List<long> Items;
        int CurrentIndex = 0;
        public ListIterator(List<long> items)
        {
            Items = items;
        }

        public long GetNext()
        {
            long returnItem = Items[CurrentIndex];
            CurrentIndex++;
            return returnItem;
        }
        public bool HasMore()
        {
            if(CurrentIndex + 1 >= Items.Count)
            {
                return false;
            }
            return true;
        }
    }
}
