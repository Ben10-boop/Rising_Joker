using System;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace RisingJokerServer
{
    //handles players trying to join the game.
    public class LobbySocket : WebSocketBehavior
    {
        //---! NEEDS FIXING !--- player needs to send his current status (red, blue, green or anon)
        //and the method should then allow already joined player to switch color or for anon to join
        protected override void OnMessage(MessageEventArgs e)
        {
            Console.WriteLine("-JoinGame- Received message from client: " + e.Data);
            try
            {
                string receivedPlayerColor = e.Data;

                PlayerColor color = (PlayerColor)Enum.Parse(typeof(PlayerColor), receivedPlayerColor);

                JoinManager.GetInstance().JoinAs(color);
                Send($"Joined as '{color}'");
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
