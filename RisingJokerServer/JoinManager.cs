using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RisingJokerServer
{
    public class JoinManager //Pattern: Singleton
    {
        private static JoinManager instance = null;
        private bool redJoined, blueJoined, greenJoined;
        private int playersJoined = 0;

        private static object threadLockInitialize = new object();
        private object threadLockRed = new object();
        private object threadLockBlue = new object();
        private object threadLockGreen = new object();
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

        public bool AttemptJoinAsRed()
        {
            lock (threadLockRed)
            {
                if (redJoined)
                {
                    return false; //you failed to join as red
                }
                else
                {
                    redJoined = true;
                    playersJoined++;
                    return true; //you successfully joined as red
                }
            }
        }
        public bool AttemptJoinAsBlue()
        {
            lock (threadLockBlue)
            {
                if (blueJoined)
                {
                    return false;
                }
                else
                {
                    blueJoined = true;
                    playersJoined++;
                    return true;
                }
            }
        }
        public bool AttemptJoinAsGreen()
        {
            lock (threadLockGreen)
            {
                if (greenJoined)
                {
                    return false;
                }
                else
                {
                    greenJoined = true;
                    playersJoined++;
                    return true;
                }
            }
        }
        public int GetPlayersJoined()
        {
            return playersJoined;
        }
    }
}
