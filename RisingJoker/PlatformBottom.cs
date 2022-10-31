using System.Drawing;

namespace RisingJoker
{
    public class PlatformBottom : MovableObject
    {
        public static string TAG = "platformBottom";
        public PlatformBottom(Size size, Point position, Color color) : base(size, position, true, color, TAG) { }

        public override void OnCollisionWith(GameObject other)
        {
        }
    }
}
