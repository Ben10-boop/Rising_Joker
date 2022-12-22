using RisingJoker.PlayerFactoryMethod;
using System;
using System.Collections.Generic;

namespace RisingJoker.RenderingAdapters
{
    public class RedPlayerHandler : BasePlayerHandler
    {
        private static RedPlayerCreator redCreator = new RedPlayerCreator();
        public override Player handle(PlayerColor chosenColor, Player mainPlayer, List<Player> opponents)
        {
            Console.WriteLine("RED COLOR");
            Player redPlayer = redCreator.CreatePlayer();
            Player newMainPlayer = mainPlayer;
            if (chosenColor == PlayerColor.Red)
            {
                newMainPlayer = redPlayer;
            }
            else
            {
                opponents.Add(redPlayer);
            }

            return handleNext(chosenColor, newMainPlayer, opponents);
        }
    }
}
