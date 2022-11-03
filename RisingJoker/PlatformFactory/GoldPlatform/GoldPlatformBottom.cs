using System;
using System.Drawing;

namespace RisingJoker.PlatformFactory
{
    public class GoldPlatformBottom : PlatformBottom
    {
        public GoldPlatformBottom(Size size, Point position, int passthroughPenalty) : base(size, position, Color.Gold, Math.Min(15 + passthroughPenalty, 0))
        {
        }
    }
}
