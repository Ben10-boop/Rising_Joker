using System;
using System.Drawing;

namespace RisingJoker.PlatformFactory
{
    public class ScaryEnemy : Enemy
    {
        public ScaryEnemy(Size size, Point position, int basePenalty) : base(Color.DarkRed, size, position, Math.Max(-10 + basePenalty, -10))
        {
        }
    }
}
