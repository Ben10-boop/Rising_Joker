using RisingJokerServer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RisingJokerServer.PlatGenTemplateMethod
{
    internal abstract class PlatformGenerator
    {
        public virtual PlatformDto GeneratePlatform()
        {
            GenerateVariables(out int platformWidth, out int platformHeight, out int platformPosition, out int platformType, out int platAmount);
            bool hasCoin = false;
            int coinPosX = 0;
            bool hasEnemy = false;
            int enemyPosX = 0;

            int nextPlatformOffset = 500 / platAmount;

            var rand = new Random();
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

            return MakeDto(platformWidth, platformHeight, platformPosition, hasCoin, hasEnemy, coinPosX, enemyPosX, platAmount, nextPlatformOffset);
        }

        protected virtual void GenerateVariables(out int width, out int height, 
            out int position, out int type, out int platAmount)
        {
            var rand = new Random();
            width = rand.Next(200, 301);
            height = rand.Next(18, 23);
            position = rand.Next(0, 501 - width);
            type = rand.Next(0, 4);
            platAmount = 1;
        }

        protected virtual PlatformDto MakeDto(int platformWidth, int platformHeight, int platformPosition,
            bool hasCoin, bool hasEnemy, int coinPosX, int enemyPosX, int platformAmount, int nextPlatformOffset)
        {
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
