using RisingJoker.Object;
using System.Drawing;

namespace RisingJoker.CoinObject
{
    public class Coin : MovableObject
    {
        public static string TAG = "coin";
        public Coin(Color color, Size size, Point position) : base(size, position, true, color, TAG) { }

        public override void OnCollisionWith(IGameObject other)
        {
        }
    }
}
