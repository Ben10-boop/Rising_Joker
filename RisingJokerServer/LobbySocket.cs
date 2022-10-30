using Newtonsoft.Json;
using RisingJokerServer.DTOs;
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
                //string receivedPlayerColor = e.Data;
                StringDto receivedPlayerColor = JsonConvert.DeserializeObject<StringDto>(e.Data);

                PlayerColor color = (PlayerColor)Enum.Parse(typeof(PlayerColor), receivedPlayerColor.Value);

                bool joinedSuccessfully = JoinManager.GetInstance().TryJoinAs(color);
                if (joinedSuccessfully)
                    Send(JsonConvert.SerializeObject(new StringDto { Value = $"Joined as {color}" }));
                Send("Player already taken");
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
