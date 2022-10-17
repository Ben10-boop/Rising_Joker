using System.Drawing;

namespace RisingJoker
{
    public class EnemyFactory
    {
        private static int height = 30;
        private static int width = 15;
        public static Enemy CreateEnemy(Point standingOn)
        {
            Point correctedPoint = new Point(standingOn.X, standingOn.Y - height);
            return new Enemy(Color.Purple, new Size(width, height), correctedPoint);
        }
    }
}
