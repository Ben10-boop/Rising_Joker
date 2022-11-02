using RisingJokerServer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RisingJokerServer.PlatGenerationStrategy
{
    internal class Lvl1PlatformStrategy : IPlatGenerationStrategy
    {
        public PlatformDto GeneratePlatform()
        {
            var rand = new Random();
            int platformWidth = rand.Next(200, 301);
            int platformHeight = rand.Next(18, 23);
            int platformPosition = rand.Next(0, 501 - platformWidth);
            int platformType = rand.Next(0, 4);
            bool hasCoin = false;
            int coinPosX = 0;
            bool hasEnemy = false;
            int enemyPosX = 0;
            switch (platformType)
            {
                case 1:
                    hasCoin = true;
                    coinPosX = rand.Next(0, platformWidth - 25);
                    break;
                case 2:
                    hasEnemy = true;
                    enemyPosX = rand.Next(0, platformWidth - 25);
                    break;
                case 3:
                    hasCoin = true;
                    coinPosX = rand.Next(0, platformWidth - 25);
                    hasEnemy = true;
                    enemyPosX = rand.Next(0, platformWidth - 25);
                    break;
            }
            PlatformDto platform = new PlatformDto
            {
                Width = platformWidth,
                Height = platformHeight,
                PositionX = platformPosition,
                HasCoin = hasCoin,
                HasEnemy = hasEnemy,
                CoinPosX = coinPosX,
                EnemyPosX = enemyPosX
            };
            return platform;
        }
    }
}
