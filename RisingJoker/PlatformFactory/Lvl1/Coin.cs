using RisingJoker.PlatformFactory;
using System.Drawing;

namespace RisingJoker
{
    public class Coin : MovableObject, ICoin
    {
        public static string TAG = "coin";
        private readonly int Value = 50;
        public Coin(Color color, Size size, Point position) : base(size, position, true, color, TAG) { }

        public override void OnCollisionWith(GameObject other)
        {
        }

        public int GetValue()
        {
            return Value;
        }
    }
}
