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

        public Coin CreateCoin(int xPosition)
        {
            Point correctedPoint = new Point(xPosition, -coinSize);
            return new Coin(Color.Orange, new Size(coinSize, coinSize), correctedPoint);
        }
        public Enemy CreateEnemy(int xPosition)
        {
            Point correctedPoint = new Point(xPosition, -enemyHeight);
            return new Enemy(ColorTranslator.FromHtml("#520719"), new Size(enemyWidth, enemyHeight), correctedPoint);
        }

        public PlatformBottom CreatePlatformBottom(int platformWidth, int platformPosX)
        {
            Point correctedPoint = new Point(platformPosX + 20, 15);
            return new PlatformBottom(new Size(platformWidth - 40, 10), correctedPoint, Color.Brown);
        }
    }
}
