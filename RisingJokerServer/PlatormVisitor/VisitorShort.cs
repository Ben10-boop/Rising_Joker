using RisingJokerServer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RisingJokerServer.PlatormVisitor
{
    internal class VisitorShort : IVisitor
    {
        public void visitPlatform(PlatformDto platform)
        {
            if (platform.PlatformAmount < 2)
            {
                Console.Write($"Plat({platform.Width}; {platform.Height}; {platform.PositionX}; {platform.HasCoin}; {platform.HasEnemy})\n");
            }
            else
            {
                Console.Write($"Arr({platform.Width}; {platform.Height}; {platform.PositionX}; {platform.HasCoin}; {platform.HasEnemy}; {platform.PlatformAmount}; {platform.NextPlatformOffset})\n");
            }
        }

        public void visitPlayerPosition(PlayerPositionDto playerPosition)
        {
            Console.Write($"PlrPos({playerPosition.PlayerColor}; {playerPosition.PositionX}; {playerPosition.PositionY})");
        }
    }
}
