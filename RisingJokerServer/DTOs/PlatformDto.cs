namespace RisingJokerServer.DTOs
{
    internal class PlatformDto
    {
        public int Level { get; set; } = 1;
        public int platformAmount { get; set; } = 1;
        public int nextPlatformOffset { get; set; } = 0;
        public int Width { get; set; }
        public int Height { get; set; }
        public int PositionX { get; set; }
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
