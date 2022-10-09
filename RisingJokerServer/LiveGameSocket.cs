using Newtonsoft.Json;
using RisingJokerServer.DTOs;
using System;
using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace RisingJokerServer
{

    //handles running the game
    public class LiveGameSocket : WebSocketBehavior
    {
        private bool gameRunning = false;

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
                PlatformDto platform = GenerateRandomPlatform();
                Sessions.Broadcast(JsonConvert.SerializeObject(platform));
                Console.WriteLine("-RunGame- Spawn platform:" + platform.ToString());
                await Task.Delay(1000);
            }

            //need to add level_2
            //...
        }
        private PlatformDto GenerateRandomPlatform()
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
}
