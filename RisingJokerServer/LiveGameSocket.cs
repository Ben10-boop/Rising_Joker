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

        //AMOUNT OF PLAYERS REQUIRED TO LAUNCH THE GAME
        private int requiredPlayerCount = 1;

        //handles the messages of players trying to start the game
        protected override void OnMessage(MessageEventArgs e)
        {
            StringDto message = JsonConvert.DeserializeObject<StringDto>(e.Data);
            Console.WriteLine("-RunGame- Received message from client: " + message.Value);
            if (gameRunning)
            {
                Send(JsonConvert.SerializeObject(new StringDto { Value = "Game has already started!" }));
                //Send("Game has already started!");
            }
            else if (JoinManager.GetInstance().GetPlayersJoined() < requiredPlayerCount)
            {
                Send(JsonConvert.SerializeObject(new StringDto { Value = "Not enough players have joined yet" }));
                //Send("Not enough players have joined yet");
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
            Sessions.Broadcast(JsonConvert.SerializeObject(new StringDto { Value = "Starting in 3" }));
            await Task.Delay(1000);
            Sessions.Broadcast(JsonConvert.SerializeObject(new StringDto { Value = "Starting in 2" }));
            await Task.Delay(1000);
            Sessions.Broadcast(JsonConvert.SerializeObject(new StringDto { Value = "Starting in 1" }));
            await Task.Delay(1000);
            Sessions.Broadcast(JsonConvert.SerializeObject(new StringDto { Value = "Game Start!" }));

            //level_1
            for (int i = 0; i < 10; i++)
            {
                PlatformDto platform;
                if (i < 7)
                {
                    platform = new PlatformDto
                    {
                        platformAmount = 3,
                        nextPlatformOffset = 100,
                        Width = 50,
                        Height = 20,
                        PositionX = 40,
                        HasCoin = true,
                        HasEnemy = false,
                        CoinPosX = 10,
                        EnemyPosX = 0
                    };
                }
                else
                {
                    platform = GenerateRandomLvl1Platform();
                }

                Sessions.Broadcast(JsonConvert.SerializeObject(platform));
                Console.WriteLine("-RunGame- Spawn platform:" + platform.ToString());
                await Task.Delay(1000);
            }
            for (int i = 0; i < 40; i++)
            {
                PlatformDto platform = GenerateRandomLvl2Platform();
                Sessions.Broadcast(JsonConvert.SerializeObject(platform));
                Console.WriteLine("-RunGame- Spawn platform:" + platform.ToString());
                await Task.Delay(1000);
            }
        }
        private PlatformDto GenerateRandomLvl1Platform()
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

        private PlatformDto GenerateRandomLvl2Platform()
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
                Level = 2,
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
