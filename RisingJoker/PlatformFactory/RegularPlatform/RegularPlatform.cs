using RisingJoker.CoinObject;
using RisingJoker.EnemyObject;
using RisingJoker.GameObjectInfoCreation;
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
            return new Coin(GameObjectInfoFactory.GetGameObjectInfo(new Size(coinSize, coinSize), Theme.CoinColor, Coin.TAG), correctedPoint, coinValue);
        }
        public IEnemy CreateEnemy(Size enemySize, int penalty)
        {
            Point correctedPoint = new Point(0, -enemySize.Width);
            return new EnemyBuilder().SetBaseEnemy(new Enemy(GameObjectInfoFactory.GetGameObjectInfo(enemySize, Theme.CoinColor, Enemy.TAG), correctedPoint, penalty)).AddWalking().GetEnemy();
        }

        public PlatformBottom CreatePlatformBottom(int platformWidth, int platformPosX, int penalty)
        {
            Point correctedPoint = new Point(platformPosX + 20, 15);
            return new PlatformBottom(GameObjectInfoFactory.GetGameObjectInfo(new Size(platformWidth - 40, 10), Theme.CoinColor, PlatformBottom.TAG), correctedPoint, penalty);
        }
    }
}
