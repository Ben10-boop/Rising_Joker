using System;
using WebSocketSharp.Server;

namespace RisingJokerServer
{
    class Program
    {
        //private static readonly string ServerAddress = "ws://127.0.0.1:6969";
        private static readonly string ServerAddress = "ws://25.44.67.63:6969"; // <- This for multiplayer
        static void Main(string[] args)
        {
            WebSocketServer wssv = new WebSocketServer(ServerAddress);
            wssv.AddWebSocketService<LobbySocket>("/JoinGame");
            wssv.AddWebSocketService<LiveGameSocket>("/RunGame");
            wssv.AddWebSocketService<PlayerPositionBroadcastSocket>("/PlayerPosBroadcast");

            wssv.Start();
            Console.WriteLine("Web Socket server started on " + ServerAddress);

            Console.ReadKey();
            wssv.Stop();
        }
    }
}
