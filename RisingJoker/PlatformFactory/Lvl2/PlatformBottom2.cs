using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RisingJoker.PlatformFactory.Lvl2
{
    internal class PlatformBottom2 : PlatformBottom
    {
        private readonly int passthroughPenalty = -10;
        public PlatformBottom2(Size size, Point position, Color color) : base(size, position, color)
        {
        }

        public override void OnCollisionWith(GameObject other)
        {
        }

        public override int GetPassthroughPenalty()
        {
            return passthroughPenalty;
        }
    }
}
