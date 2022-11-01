using RisingJoker.PlatformFactory;
using System.Drawing;

namespace RisingJoker
{
    public class PlatformBottom : MovableObject, IBottom
    {
        public static string TAG = "pBottom";
        private readonly int passthroughPenalty = -53;
        public PlatformBottom(Size size, Point position, Color color) : base(size, position, true, color, TAG) 
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
