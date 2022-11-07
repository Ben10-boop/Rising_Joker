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

            Console.ReadKey();
            wssv.Stop();
        }
    }

}
