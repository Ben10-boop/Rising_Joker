using RisingJokerServer.PlatormVisitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RisingJokerServer.DTOs
{
    internal class PlayerPositionDto : IVisitable
    {
        public string PlayerColor { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }

        public void Accept(IVisitor visitor)
        {
            visitor.visitPlayerPosition(this);
        }
    }
}
