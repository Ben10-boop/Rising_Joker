using RisingJoker.EnemyObject;
using System;
using System.Drawing;

namespace RisingJoker.PlatformFactory
{
    internal class ScaryPlatform : IPlatFactory
    {
        public Coin CreateCoin(int coinSize, int baseCoinValue)
        {
            Point correctedPoint = new Point(0, -coinSize);
            return new Coin(Color.DarkRed, new Size(coinSize, coinSize), correctedPoint, Math.Max(baseCoinValue - 10, 0));
        }
        public IEnemy CreateEnemy(Size enemySize, int basePenalty)
        {
            Point correctedPoint = new Point(0, -enemySize.Width);

            return new EnemyBuilder().SetBaseEnemy(new Enemy(Color.DarkRed, enemySize, correctedPoint, Math.Max(-10 + basePenalty, -10)))
                .AddHovering().AddWalking().AddTeleporting().GetEnemy();

        }

        public PlatformBottom CreatePlatformBottom(int platformWidth, int platformPosX, int basePenalty)
        {
            Point correctedPoint = new Point(platformPosX + 20, 15);
            return new PlatformBottom(new Size(platformWidth - 40, 10), correctedPoint, Color.DarkRed, Math.Min(-15 + basePenalty, -10));
        }
    }
}
