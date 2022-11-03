using System;
using System.Drawing;

namespace RisingJoker.PlatformFactory
{
    public class GoldEnemy : Enemy
    {
        public GoldEnemy(Size size, Point position, int basePenalty) : base(Color.LightPink, size, position, Math.Min(10 + basePenalty, 0))
        {
        }
    }
}
