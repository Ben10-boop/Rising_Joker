using RisingJoker.CoinObject;
using RisingJoker.EnemyObject;
using RisingJoker.PlatformsBuilder;
using System.Drawing;

namespace RisingJoker.PlatformFactory
{
    internal class RegularPlatform : IPlatFactory
    {
        private PlatformColorTheme Theme;

        public RegularPlatform(PlatformColorTheme theme)
        {
            Theme = theme;
        }

        public Coin CreateCoin(int coinSize, int coinValue)
        {
            Point correctedPoint = new Point(0, -coinSize);
            return new Coin(Theme.CoinColor, new Size(coinSize, coinSize), correctedPoint, coinValue);
        }
        public IEnemy CreateEnemy(Size enemySize, int penalty)
        {
            Point correctedPoint = new Point(0, -enemySize.Width);
            return new EnemyBuilder().SetBaseEnemy(new Enemy(Theme.EnemyColor, enemySize, correctedPoint, penalty)).AddWalking().GetEnemy();
        }

        public PlatformBottom CreatePlatformBottom(int platformWidth, int platformPosX, int penalty)
        {
            Point correctedPoint = new Point(platformPosX + 20, 15);
            return new PlatformBottom(new Size(platformWidth - 40, 10), correctedPoint, Theme.MainColor, penalty);
        }
    }
}
