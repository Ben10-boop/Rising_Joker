using System;
using WebSocketSharp.Server;

namespace RisingJokerServer
{
    class Program
    {
        static void Main(string[] args)
        {
            WebSocketServer wssv = new WebSocketServer("ws://127.0.0.1:6969");
            wssv.AddWebSocketService<LobbySocket>("/JoinGame");
            wssv.AddWebSocketService<LiveGameSocket>("/RunGame");
            wssv.AddWebSocketService<PlayerPositionBroadcastSocket>("/PlayerPosBroadcast");

            wssv.Start();
            Console.WriteLine("Web Socket server started on ws://127.0.0.1:6969");

            Console.ReadKey();
            wssv.Stop();
        }
    }

}
