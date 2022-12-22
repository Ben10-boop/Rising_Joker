using RisingJoker.PlayerFactoryMethod;
using System;
using System.Collections.Generic;

namespace RisingJoker.RenderingAdapters
{
    public class BluePlayerHandler : BasePlayerHandler
    {
        private static BluePlayerCreator blueCreator = new BluePlayerCreator();
        public override Player handle(PlayerColor chosenColor, Player mainPlayer, List<Player> opponents)
        {
            Console.WriteLine("BLUE COLOR");

            Player bluePlayer = blueCreator.CreatePlayer();
            Player newMainPlayer = mainPlayer;
            if (chosenColor == PlayerColor.Blue)
            {
                newMainPlayer = bluePlayer;
            }
            else
            {
                opponents.Add(bluePlayer);
            }

            return handleNext(chosenColor, newMainPlayer, opponents);
        }
    }
}
