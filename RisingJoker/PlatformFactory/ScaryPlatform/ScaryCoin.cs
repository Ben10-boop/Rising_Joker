using RisingJoker.CoinObject;
using RisingJoker.GameObjectInfoCreation;
using System;
using System.Drawing;

namespace RisingJoker.PlatformFactory
{
    public class ScaryCoin : Coin
    {
        public ScaryCoin(Size size, Point position, int coinValue) : base(GameObjectInfoFactory.GetGameObjectInfo(size, Color.DarkGoldenrod, Coin.TAG), position, Math.Max(coinValue - 10, 0))
        {
        }
    }
}
