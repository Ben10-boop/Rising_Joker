using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RisingJoker.PlatformFactory.Lvl2
{
    internal class Lvl2PlatFactory : IPlatFactory
    {
        private readonly int coinSize = 20;

        private readonly int enemyHeight = 15;
        private readonly int enemyWidth = 35;

        public Coin CreateCoin()
        {
            Point correctedPoint = new Point(0, -coinSize);
            return new Coin2(Color.Orange, new Size(coinSize, coinSize), correctedPoint);
        }
        public Enemy CreateEnemy()
        {
            Point correctedPoint = new Point(0, -enemyHeight);
            return new Enemy2(ColorTranslator.FromHtml("#520719"), new Size(enemyWidth, enemyHeight), correctedPoint);
        }

        public PlatformBottom CreatePlatformBottom(int platformWidth, int platformPosX)
        {
            Point correctedPoint = new Point(platformPosX + 20, 15);
            return new PlatformBottom2(new Size(platformWidth - 40, 10), correctedPoint, Color.Brown);
        }
    }
}
