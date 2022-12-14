using RisingJokerServer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RisingJokerServer.PlatormVisitor
{
    internal class VisitorExtended : IVisitor
    {
        public void visitPlatform(PlatformDto platform)
        {
            if (platform.PlatformAmount < 2)
            {
                Console.Write($"Platform(\n" +
                    $"Width: {platform.Width};\n" +
                    $"Height: {platform.Height};\n" +
                    $"Position: {platform.PositionX};\n" +
                    $"Coin: {platform.HasCoin}" + (platform.HasCoin?$", psition: {platform.CoinPosX}\n":";\n") +
                    $"Enemy: {platform.HasEnemy}" + (platform.HasEnemy ? $", psition: {platform.EnemyPosX})\n" : ")\n"));

            }
            else
            {
                Console.Write($"Array(\n" +
                    $"Width: {platform.Width};\n" +
                    $"Height: {platform.Height};\n" +
                    $"Position: {platform.PositionX};\n" +
                    $"Platform amount: {platform.PlatformAmount}; Platform offset: {platform.NextPlatformOffset};\n" +
                    $"Coin: {platform.HasCoin}" + (platform.HasCoin ? $", psition: {platform.CoinPosX}\n" : ";\n") +
                    $"Enemy: {platform.HasEnemy}" + (platform.HasEnemy ? $", psition: {platform.EnemyPosX})\n" : ")\n"));
            }
        }

        public void visitPlayerPosition(PlayerPositionDto playerPosition)
        {
            Console.Write($"PlayerPosition(\n" +
                $"Color: {playerPosition.PlayerColor};\n" +
                $"X coordinate: {playerPosition.PositionX};\n" +
                $"Y coordinate: {playerPosition.PositionY})\n");
        }
    }
}
