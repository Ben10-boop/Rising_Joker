using RisingJoker.GameObjectInfoCreation;
using System;
using System.Drawing;

namespace RisingJoker.PlatformFactory
{
    public class GoldPlatformBottom : PlatformBottom
    {
        public GoldPlatformBottom(Size size, Point position, int passthroughPenalty) : base(GameObjectInfoFactory.GetGameObjectInfo(size, Color.Gold, PlatformBottom.TAG), position, Math.Min(15 + passthroughPenalty, 0))
        {
        }
    }
}
