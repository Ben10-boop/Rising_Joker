using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RisingJoker.PlatformFactory.Lvl2
{
    internal class PlatformBottom2 : MovableObject, IBottom
    {
        public static string TAG = "pBottom";
        private readonly int passthroughPenalty = -73;
        public PlatformBottom2(Size size, Point position, Color color) : base(size, position, true, color, TAG)
        {
        }

        public override void OnCollisionWith(GameObject other)
        {
        }

        public int GetPassthroughPenalty()
        {
            return passthroughPenalty;
        }
    }
}
