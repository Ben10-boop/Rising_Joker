using RisingJoker.PlayerFactoryMethod;
using System;
using System.Collections.Generic;

namespace RisingJoker.RenderingAdapters
{
    public class GreenPlayerHandler : BasePlayerHandler
    {
        private static GreenPlayerCreator greenCreator = new GreenPlayerCreator();
        public override Player handle(PlayerColor chosenColor, Player mainPlayer, List<Player> opponents)
        {
            Console.WriteLine("GREEN COLOR");

            Player greenPlayer = greenCreator.CreatePlayer();
            Player newMainPlayer = mainPlayer;
            if (chosenColor == PlayerColor.Green)
            {
                newMainPlayer = greenPlayer;
            }
            else
            {
                opponents.Add(greenPlayer);
            }

            return handleNext(chosenColor, newMainPlayer, opponents);
        }
    }
}
