using RisingJoker.CoinObject;
using RisingJoker.GameObjectInfoCreation;
using System;
using System.Drawing;

namespace RisingJoker.PlatformFactory
{
    public class ScaryEnemy : Enemy
    {
        public ScaryEnemy(Size size, Point position, int basePenalty) : base(GameObjectInfoFactory.GetGameObjectInfo(size, Color.DarkRed, Coin.TAG), position, Math.Max(-10 + basePenalty, -10))
        {
        }
    }
}
