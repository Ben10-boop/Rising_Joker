using RisingJoker.PlatformFactory;
using System.Drawing;

namespace RisingJoker
{
    public abstract class PlatformBottom : MovableObject
    {
        public static string TAG = "pBottom";
        public PlatformBottom(Size size, Point position, Color color) : base(size, position, true, color, TAG) 
        {
        }

        public override void OnCollisionWith(GameObject other)
        {
        }

        public virtual int GetPassthroughPenalty()
        {
            return 0;
        }
    }
}
