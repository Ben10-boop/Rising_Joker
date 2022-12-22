using RisingJokerServer.DTOs;
using System;

namespace RisingJokerServer.PlatGenTemplateMethod
{
    internal sealed class Lvl2ArrayGenerator : PlatformGenerator
    {
        protected override void GenerateVariables(out int width, out int height, out int position, out int type, out int platAmount)
        {
            var rand = new Random();

            platAmount = rand.Next(2, 5);
            //platAmount = 4;
            width = rand.Next(175 / platAmount, 276 / platAmount);
            height = rand.Next(18, 23);
            position = (500 / platAmount) - width;
            type = rand.Next(0, 4);
            //type = 3;
        }
        protected override PlatformDto MakeDto(int platformWidth, int platformHeight, int platformPosition,
            bool hasCoin, bool hasEnemy, int coinPosX, int enemyPosX, int platformAmount, int nextPlatformOffset)
        {
            PlatformDto platform = new PlatformDto
            {
                PlatformAmount = platformAmount,
                NextPlatformOffset = nextPlatformOffset,
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
