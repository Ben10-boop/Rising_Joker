using RisingJokerServer.Iterator;
using System;
using WebSocketSharp.Server;

namespace RisingJokerServer
{
    class Program
    {
        static void Main(string[] args)
        {
            WebSocketServer wssv = new WebSocketServer("ws://127.0.0.1:6969");
            //WebSocketServer wssv = new WebSocketServer("ws://25.44.67.63:6969"); // <- This for multiplayer
            wssv.AddWebSocketService<LobbySocket>("/JoinGame");
            wssv.AddWebSocketService<LiveGameSocket>("/RunGame");
            wssv.AddWebSocketService<PlayerPositionBroadcastSocket>("/PlayerPosBroadcast");

            wssv.Start();
            Console.WriteLine("Web Socket server started on ws://" + wssv.Address + ":6969");

            while (true)
            {
                Console.WriteLine("Available commands:\n " +
                    "\"p\" to print generation times;\n" +
                    "\"l\" to set the data collection type to List and use regular iterator;\n" +
                    "\"r\" to set the data collection type to List and use reverse iterator;\n" +
                    "\"s\" to set the data collection type to LinkedList");
                string stuff = Console.ReadLine();
                switch (stuff)
                {
                    case "p":
                        LiveGameSocket.PrintGenerationTimerResults();
                        break;
                    case "l":
                        LiveGameSocket.OverrideGenTimes(new IterableList());
                        LiveGameSocket.useReverseIterator = false;
                        break;
                    case "r":
                        LiveGameSocket.OverrideGenTimes(new IterableList());
                        LiveGameSocket.useReverseIterator = true;
                        break;
                    case "s":
                        LiveGameSocket.OverrideGenTimes(new IterableLinkedList());
                        LiveGameSocket.useReverseIterator = false;
                        break;
                }
            }
            wssv.Stop();
        }
    }

}
