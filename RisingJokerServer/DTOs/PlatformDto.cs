using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RisingJokerServer.DTOs
{
    internal class PlatformDto
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int PositionX { get; set; }
        public override string ToString()
        {
            return "W: " + Width + "; H: " + Height + "; X: " + PositionX;
        }
    }
}
