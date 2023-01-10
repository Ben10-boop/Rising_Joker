using System;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace RisingJokerServer
{
    //handles players trying to join the game.
    public class LobbySocket : WebSocketBehavior
    {
        protected override void OnMessage(MessageEventArgs e)
        {
            Console.WriteLine("-JoinGame- Received message from client: " + e.Data);
            try
            {
                string receivedPlayerColor = e.Data;

                PlayerColor color = (PlayerColor)Enum.Parse(typeof(PlayerColor), receivedPlayerColor);

                if (JoinManager.TryJoinAs(color))
                {
                    Send($"Joined as '{color}'");
                    Sessions.Broadcast($"'{color}' player joined the game");
                }
                else
                {
                    Send($"'{color}' is already taken!");
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Wrong data");
                Send("Wrong data");
                return;
            }
        }
    }
}
