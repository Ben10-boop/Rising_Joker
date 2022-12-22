using RisingJoker.CoinObject;
using RisingJoker.GameObjectInfoCreation;
using System;
using System.Drawing;

namespace RisingJoker.PlatformFactory
{
    public class ScaryPlatformBottom : PlatformBottom
    {
        public ScaryPlatformBottom(Size size, Point position, int passthroughPenalty) : base(GameObjectInfoFactory.GetGameObjectInfo(size, Color.DarkRed, Coin.TAG), position, Math.Min(-15 + passthroughPenalty, -10))
        {
        }
    }
}
