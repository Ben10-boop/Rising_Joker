using System.Drawing;

namespace RisingJoker.CoinObject
{
    public class CoinFactory
    {
        private static int size = 25;
        public static Coin CreateCoin(int xPosition)
        {
            Point correctedPoint = new Point(xPosition, -size);
            return new Coin(Color.Yellow, new Size(size, size), correctedPoint);
        }
    }
}
