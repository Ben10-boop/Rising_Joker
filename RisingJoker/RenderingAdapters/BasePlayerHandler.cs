using System.Collections.Generic;

namespace RisingJoker.RenderingAdapters
{
    public abstract class BasePlayerHandler : IPlayerHandler
    {
        private BasePlayerHandler next;

        public BasePlayerHandler setNextHandler(BasePlayerHandler next)
        {
            this.next = next;
            return next;
        }

        public abstract Player handle(PlayerColor chosenColor, Player mainPlayer, List<Player> opponents);

        protected Player handleNext(PlayerColor chosenColor, Player mainPlayer, List<Player> opponents)
        {
            if (next == null)
            {
                return mainPlayer;
            }

            return next.handle(chosenColor, mainPlayer, opponents);
        }
    }
}
