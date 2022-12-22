using RisingJoker.PlayerFactoryMethod;
using System;
using System.Collections.Generic;

namespace RisingJoker.RenderingAdapters
{
    public class SpectatorPlayerHandler : BasePlayerHandler
    {
        private static RedPlayerCreator redCreator = new RedPlayerCreator();
        public override Player handle(PlayerColor chosenColor, Player mainPlayer, List<Player> opponents)
        {
            Console.WriteLine("SPECTATOR COLOR");

            Player newMainPlayer = mainPlayer;
            if (chosenColor == PlayerColor.None)
            {
                newMainPlayer = redCreator.CreatePlayer();
            }

            return handleNext(chosenColor, newMainPlayer, opponents);
        }
    }
}
