using RisingJokerServer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RisingJokerServer.PlatormVisitor
{
    internal class VisitorStandard : IVisitor
    {
        public void visitPlatform(PlatformDto platform)
        {
            if(platform.PlatformAmount < 2)
            {
                Console.Write($"Platform(width: {platform.Width}; height: {platform.Height}; position: {platform.PositionX}; coin: {platform.HasCoin}; enemy: {platform.HasEnemy}) \n");
            }
            else
            {
                Console.Write($"Array(width: {platform.Width}; height: {platform.Height}; position: {platform.PositionX}; coin: {platform.HasCoin}; enemy: {platform.HasEnemy};\nAmount: {platform.PlatformAmount}; offset: {platform.NextPlatformOffset}) \n");
            }
        }

        public void visitPlayerPosition(PlayerPositionDto playerPosition)
        {
            Console.Write($"PlayerPosition(color: {playerPosition.PlayerColor}; x: {playerPosition.PositionX}; y: {playerPosition.PositionY})");
        }
    }
}
