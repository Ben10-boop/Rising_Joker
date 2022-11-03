using RisingJoker.CoinObject;
using System;
using System.Drawing;

namespace RisingJoker.PlatformFactory
{
    public class ScaryCoin : Coin
    {
        public ScaryCoin(Size size, Point position, int coinValue) : base(Color.DarkGoldenrod, size, position, Math.Max(coinValue - 10, 0))
        {
        }
    }
}
