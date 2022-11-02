using RisingJoker.BaseGameObjects;
using System.Drawing;

namespace RisingJoker.PlatformFactory
{
    public class Coin : MovableObject, ICloneable<Coin>, IPoints
    {
        public static string TAG = "coin";

        public int Points { get; }

        public Coin(Color color, Size size, Point position, int coinValue) : base(size, position, true, color, TAG)
        {
            Points = coinValue;
        }

        public Coin Clone()
        {
            return (Coin)MemberwiseClone();
        }
    }
}
