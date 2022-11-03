using RisingJoker.CoinObject;
using RisingJoker.EnemyObject;
using System.Drawing;

namespace RisingJoker.PlatformFactory
{
    internal class ScaryPlatform : IPlatFactory
    {
        public Coin CreateCoin(int coinSize, int baseCoinValue)
        {
            Point correctedPoint = new Point(0, -coinSize);
            return new ScaryCoin(new Size(coinSize, coinSize), correctedPoint, baseCoinValue);
        }
        public IEnemy CreateEnemy(Size enemySize, int basePenalty)
        {
            Point correctedPoint = new Point(0, -enemySize.Width);

            EnemyBuilder builder = new EnemyBuilder();
            builder.SetBaseEnemy(new ScaryEnemy(enemySize, correctedPoint, basePenalty));
            builder.AddHovering().AddWalking().AddTeleporting().GetEnemy();
            return builder.GetEnemy();
        }

        public PlatformBottom CreatePlatformBottom(int platformWidth, int platformPosX, int basePenalty)
        {
            Point correctedPoint = new Point(platformPosX + 20, 15);
            return new ScaryPlatformBottom(new Size(platformWidth - 40, 10), correctedPoint, basePenalty);
        }
    }
}
