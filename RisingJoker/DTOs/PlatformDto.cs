namespace RisingJoker.DTOs
{
    internal class PlatformDto
    {
        public int Width { get; set; } = 350;
        public int Height { get; set; } = 20;
        public int PositionX { get; set; } = 10;
        public bool HasCoin { get; set; } = false;
        public bool HasEnemy { get; set; } = false;
        public int CoinPosX { get; set; } = 0;
        public int EnemyPosX { get; set; } = 0;

        public override string ToString()
        {
            return "W: " + Width + "; H: " + Height + "; X: " + PositionX + " Has Coin: " + HasCoin.ToString() + " Has Enemy: " + HasEnemy.ToString();
        }
    }
}
