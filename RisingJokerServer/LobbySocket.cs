using Newtonsoft.Json;
using RisingJokerServer.DTOs;
using RisingJokerServer.LobbyCommand;
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
        private PlayerColor _prevColor = PlayerColor.None;
        protected override void OnMessage(MessageEventArgs e)
        {
            Console.WriteLine("-JoinGame- Received message from client: " + e.Data);
            try
            {
                //string receivedPlayerColor = e.Data;
                StringDto receivedPlayerColor = JsonConvert.DeserializeObject<StringDto>(e.Data);
                PlayerColor color = (PlayerColor)Enum.Parse(typeof(PlayerColor), receivedPlayerColor.Value);

                Invoker invoker = new Invoker();
                Receiver receiver = new Receiver();


                if (color == PlayerColor.None)
                {
                    invoker.UndoPlayerType(new PlayerSelector(receiver, color));
                    invoker.SelectOrUndoPlayer(_prevColor);
                    _prevColor = PlayerColor.None;
                }
                else
                {
                    invoker.SelectPlayerType(new PlayerSelector(receiver, color));
                    invoker.SelectOrUndoPlayer(color);
                    _prevColor = color;
                    Send(JsonConvert.SerializeObject(new StringDto { Value = $"Joined as {color}" }));
                    Sessions.Broadcast(JsonConvert.SerializeObject(new StringDto { Value = $"{color} player joined the game" }));
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
