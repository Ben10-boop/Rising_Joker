using System.Collections.Generic;

namespace RisingJoker.RenderingAdapters
{
    internal interface IPlayerHandler
    {
        Player handle(PlayerColor chosenColor, Player mainPlayer, List<Player> opponents);
    }
}
