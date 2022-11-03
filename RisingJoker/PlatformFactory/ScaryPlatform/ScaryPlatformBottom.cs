using System;
using System.Drawing;

namespace RisingJoker.PlatformFactory
{
    public class ScaryPlatformBottom : PlatformBottom
    {
        public ScaryPlatformBottom(Size size, Point position, int passthroughPenalty) : base(size, position, Color.DarkRed, Math.Min(-15 + passthroughPenalty, -10))
        {
        }
    }
}
