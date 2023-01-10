﻿using Newtonsoft.Json;
using RisingJokerServer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace RisingJokerServer
{
    //Handles the multiplayer side of the game (telling everyone where opponents are located)
    internal class PlayerPositionBroadcastSocket : WebSocketBehavior
    {
        //Need 3 computers to fully test out so green is ignored so that only two computers would be required
        static PlayerPositionDto BluePos;
        static PlayerPositionDto RedPos;
        PlayerPositionDto GreenPos = new PlayerPositionDto { PositionX = 0, PositionY = 0, PlayerColor = PlayerColor.Green.ToString() };
        protected override void OnMessage(MessageEventArgs e)
        {
            PlayerPositionDto position = JsonConvert.DeserializeObject<PlayerPositionDto>(e.Data);
            switch (position.PlayerColor)
            {
                case "Blue":
                    BluePos = position;
                    break;
                case "Red":
                    RedPos = position;
                    break;
                case "Green":
                    GreenPos = position;
                    break;
            }
            if (BluePos != null && RedPos != null /*&& GreenPos != null*/)
            {
                PlayerPositionDto[] positions = new PlayerPositionDto[]
                {
                    RedPos,
                    GreenPos,
                    BluePos
                };
                Sessions.Broadcast(JsonConvert.SerializeObject(positions));
                BluePos = null;
                RedPos = null;
                //GreenPos = null;
            }
        }
    }
}
