using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RisingJoker.PlatformFactory.Lvl1
{
    internal class Lvl1PlatFactory : IPlatFactory
    {
        private readonly int coinSize = 25;

        private readonly int enemyHeight = 30;
        private readonly int enemyWidth = 15;

        public Coin CreateCoin(int xPosition)
        {
            Point correctedPoint = new Point(xPosition, -coinSize);
            return new Coin(Color.Yellow, new Size(coinSize, coinSize), correctedPoint);
        }
        public Enemy CreateEnemy(int xPosition)
        {
            Point correctedPoint = new Point(xPosition, -enemyHeight);
            return new Enemy(Color.Purple, new Size(enemyWidth, enemyHeight), correctedPoint);
        }

        public PlatformBottom CreatePlatformBottom(int platformWidth, int platformPosX)
        {
            Point correctedPoint = new Point(platformPosX + 20, 15);
            return new PlatformBottom(new Size(platformWidth - 40, 10), correctedPoint, Color.Brown);
        }
    }
}
