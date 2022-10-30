using System;
using System.Collections.Generic;

public enum PlayerColor
{
    Red = 0,
    Green = 1,
    Blue = 2,
}

namespace RisingJokerServer
{
    public class JoinManager //Pattern: Singleton
    {
        private static JoinManager instance = null;
        private readonly Dictionary<PlayerColor, bool> colorsJoined = new Dictionary<PlayerColor, bool>();

        private static object threadLockInitialize = new object();
        private object joinThreadLock = new object();

        private JoinManager()
        {
            Console.WriteLine("-JoinGame- JoinManager initialised");
        }

        public static JoinManager GetInstance()
        {
            lock (threadLockInitialize)
            {
                if (instance == null)
                {
                    instance = new JoinManager();
                }
            }
            return instance;
        }

        /// <summary>
        /// Attempt to join the game as a specified player, returns true if joined successfuly
        /// </summary>
        /// <param name="color">Color of player to join as</param>
        public bool TryJoinAs(PlayerColor color)
        {
            lock (joinThreadLock)
            {
                if (colorsJoined.ContainsKey(color))
                    return false;

                colorsJoined[color] = true;
                return true;
            }
        }

        public int GetPlayersJoined()
        {
            return colorsJoined.Count;
        }
    }
}
