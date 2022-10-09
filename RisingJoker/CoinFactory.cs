using System.Drawing;

namespace RisingJoker
{
    public class CoinFactory
    {
        private static int size = 25;
        public static Coin CreateCoin(Point standingOn)
        {
            Point correctedPoint = new Point(standingOn.X, standingOn.Y - size);
            return new Coin(Color.Yellow, new Size(size, size), correctedPoint);
        }
    }
}
