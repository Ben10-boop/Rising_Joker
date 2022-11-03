using RisingJoker.CoinObject;
using System.Drawing;

namespace RisingJoker.PlatformFactory
{
    public class GoldCoin : Coin
    {
        public GoldCoin(Size size, Point position, int coinValue) : base(Color.Gold, size, position, coinValue + 5)
        {
        }
    }
}
