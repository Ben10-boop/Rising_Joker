using System;
using System.Collections.Generic;
using System.Linq;

public enum PlayerColor
{
    Red = 0,
    Green = 1,
    Blue = 2,
    None = 3
}

namespace RisingJokerServer
{
    //stores the data of what players have already joined
    public static class JoinManager
    {
        private static Dictionary<PlayerColor, bool> colorsJoined = new Dictionary<PlayerColor, bool>();

        /*
        Attempt to join as the color specified in the argument. If joining succeeds, method will
        return true and if joining fails, the method will return false.
         */
        public static bool TryJoinAs(PlayerColor color)
        {
            if (colorsJoined.ContainsKey(color))
                return false;

            colorsJoined[color] = true;
            return true;
        }

        public static int GetPlayersJoined()
        {
            return colorsJoined.Where(item => item.Key != PlayerColor.None && item.Value).Count();
        }
    }
}
