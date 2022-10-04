using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RisingJoker.DTOs
{
    internal class PlatformDto
    {
        public int Width { get; set; } = 250;
        public int Height { get; set; } = 20;
        public int PositionX { get; set; } = 10;
        public bool HasCoin { get; set; } = false;
        public bool HasEnemy { get; set; } = false;
        public int CoinPosX { get; set; } = 0;
        public int EnemyPosX { get; set; } = 0;
    }
}
