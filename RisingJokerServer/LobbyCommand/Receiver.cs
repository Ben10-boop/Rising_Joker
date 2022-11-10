using Newtonsoft.Json;
using RisingJokerServer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp.Server;

namespace RisingJokerServer.LobbyCommand
{
    public class Receiver 
    {
        public void SelectColor(PlayerColor color)
        {
            JoinManager.GetInstance().TryJoinAs(color);
        }

        public void UndoColorSelection(PlayerColor color)
        {
            JoinManager.GetInstance().RemovePlayerJoined(color);
        }
    }
}
