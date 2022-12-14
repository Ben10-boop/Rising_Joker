using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RisingJokerServer.PlatormVisitor
{
    internal interface IVisitable
    {
        void Accept(IVisitor visitor);
    }
}
