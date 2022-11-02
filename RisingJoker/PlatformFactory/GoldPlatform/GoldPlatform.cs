using RisingJoker.EnemyObject;
using System;
using System.Drawing;

namespace RisingJoker.PlatformFactory
{
    internal class GoldPlatform : IPlatFactory
    {
        public Coin CreateCoin(int coinSize, int baseCoinValue)
        {
            Point correctedPoint = new Point(0, -coinSize);
            return new Coin(Color.Gold, new Size(coinSize, coinSize), correctedPoint, baseCoinValue);
        }
        public IEnemy CreateEnemy(Size enemySize, int basePenalty)
        {
            Point correctedPoint = new Point(0, -enemySize.Width);

            return new EnemyBuilder().SetBaseEnemy(new Enemy(Color.Gold, enemySize, correctedPoint, Math.Min(10 + basePenalty, 0))).AddHovering().GetEnemy();
        }

        public PlatformBottom CreatePlatformBottom(int platformWidth, int platformPosX, int basePenalty)
        {
            Point correctedPoint = new Point(platformPosX + 20, 15);
            return new PlatformBottom(new Size(platformWidth - 40, 10), correctedPoint, Color.Gold, Math.Min(15 + basePenalty, 0));
        }
    }
}
