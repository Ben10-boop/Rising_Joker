using Newtonsoft.Json;
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
    class Program
    {
        public static bool gameRunning;

        //handles players trying to join the game.
        public class JoinGame : WebSocketBehavior
        {
            //---! NEEDS FIXING !--- player needs to send his current status (red, blue, green or anon)
            //and the method should then allow already joined player to switch color or for anon to join
            protected override void OnMessage(MessageEventArgs e)
            {
                Console.WriteLine("-JoinGame- Received message from client: " + e.Data);
                JoinManager joinManager = JoinManager.GetInstance();
                switch (e.Data)
                {
                    case "I want to be red":
                        if (joinManager.AttemptJoinAsRed())
                        {
                            Console.WriteLine("-JoinGame- Player joined as Red");
                            Send("Ok you red");
                        }
                        else
                        {
                            Console.WriteLine("-JoinGame- Player failed to join as Red");
                            Send("Red player is already taken");
                        }
                        break;
                    case "I want to be blue":
                        if (joinManager.AttemptJoinAsBlue())
                        {
                            Console.WriteLine("-JoinGame- Player joined as Blue");
                            Send("Ok you blue");
                        }
                        else
                        {
                            Console.WriteLine("-JoinGame- Player failed to join as Blue");
                            Send("Blue player is already taken");
                        }
                        break;
                    case "I want to be green":
                        if (joinManager.AttemptJoinAsGreen())
                        {
                            Console.WriteLine("-JoinGame- Player joined as Green");
                            Send("Ok you green");
                        }
                        else
                        {
                            Console.WriteLine("-JoinGame- Player failed to join as Green");
                            Send("Green player is already taken");
                        }
                        break;
                }
            }
        }

        //handles running the game
        public class RunGame : WebSocketBehavior
        {
            //handles the messages of players trying to start the game
            protected override void OnMessage(MessageEventArgs e)
            {
                Console.WriteLine("-RunGame- Received message from client: " + e.Data);
                if (gameRunning)
                {
                    Send("Game has already started!");
                }
                else if (JoinManager.GetInstance().GetPlayersJoined() < 1)
                {
                    Send("Not enough players have joined yet");
                }
                else
                {
                    gameRunning = true;
                    DoGameRunning();
                }
            }

            //handles the server side of running the game (tells what platforms to spawn)
            private async void DoGameRunning()
            {
                Sessions.Broadcast("Starting in 3");
                await Task.Delay(1000);
                Sessions.Broadcast("Starting in 2");
                await Task.Delay(1000);
                Sessions.Broadcast("Starting in 1");
                await Task.Delay(1000);
                Sessions.Broadcast("Game Start!");

                //level_1
                for (int i = 0; i < 50; i++)
                {
                    PlatformDto platform = GeneratePlatform();
                    Sessions.Broadcast(JsonConvert.SerializeObject(platform));
                    Console.WriteLine("-RunGame- Spawn platform:" + platform.ToString());
                    await Task.Delay(1000);
                }

                //need to add level_2
                //...
            }
            private PlatformDto GeneratePlatform()
            {
                var rand = new Random();
                int platformWidth = rand.Next(200, 300);
                int platformHeight = rand.Next(18, 23);
                int platformPosition = rand.Next(0, 500 - platformWidth);
                int platformType = rand.Next(0, 4);
                bool hasCoin = false;
                int coinPosX = 0;
                bool hasEnemy = false;
                int enemyPosX = 0;
                switch (platformType)
                {
                    case 1:
                        hasCoin = true;
                        coinPosX = rand.Next(0, platformWidth - 25);
                        break;
                    case 2:
                        hasEnemy = true;
                        enemyPosX = rand.Next(0, platformWidth - 25);
                        break;
                    case 3:
                        hasCoin = true;
                        coinPosX = rand.Next(0, platformWidth - 25);
                        hasEnemy = true;
                        enemyPosX = rand.Next(0, platformWidth - 25);
                        break;
                }
                PlatformDto platform = new PlatformDto
                {
                    Width = platformWidth,
                    Height = platformHeight,
                    PositionX = platformPosition,
                    HasCoin = hasCoin,
                    HasEnemy = hasEnemy,
                    CoinPosX = coinPosX,
                    EnemyPosX = enemyPosX
                };
                return platform;
            }
        }
        static void Main(string[] args)
        {
            WebSocketServer wssv = new WebSocketServer("ws://127.0.0.1:6969");
            wssv.AddWebSocketService<JoinGame>("/JoinGame");
            wssv.AddWebSocketService<RunGame>("/RunGame");

            wssv.Start();
            Console.WriteLine("Web Socket server started on ws://127.0.0.1:6969");

            Console.ReadKey();
            wssv.Stop();
        }
    }
}
