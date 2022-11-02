using RisingJoker.BaseGameObjects;
using System.Drawing;

namespace RisingJoker.PlatformFactory
{
    public class PlatformBottom : MovableObject, IPoints
    {
        public static string TAG = "pBottom";
        public int Points { get; }
        public PlatformBottom(Size size, Point position, Color color, int passthroughPenalty) : base(size, position, true, color, TAG)
        {
            Points = passthroughPenalty;
        }
    }
}
