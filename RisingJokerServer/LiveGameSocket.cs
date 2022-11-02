using Newtonsoft.Json;
using RisingJokerServer.DTOs;
using RisingJokerServer.PlatGenerationStrategy;
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

            PlatGenerationContext platGenContext = new PlatGenerationContext();
            Random rand = new Random();

            IPlatGenerationStrategy platformStrategy = new Lvl1PlatformStrategy();
            IPlatGenerationStrategy arrayStrategy = new Lvl1ArrayStrategy();

            //level_1
            for (int i = 0; i < 10; i++)
            {
                if(rand.Next(0, 10) < 7)
                {
                    platGenContext.SetStrategy(platformStrategy);
                }
                else
                {
                    platGenContext.SetStrategy(arrayStrategy);
                }
                PlatformDto platform = platGenContext.GeneratePlatform();

                Sessions.Broadcast(JsonConvert.SerializeObject(platform));
                Console.WriteLine("-RunGame- Spawn platform:" + platform.ToString());
                await Task.Delay(1000);
            }

            platformStrategy = new Lvl2PlatformStrategy();
            arrayStrategy = new Lvl2ArrayStrategy();

            for (int i = 0; i < 40; i++)
            {
                if (rand.Next(0, 10) < 7)
                {
                    platGenContext.SetStrategy(platformStrategy);
                }
                else
                {
                    platGenContext.SetStrategy(arrayStrategy);
                }
                PlatformDto platform = platGenContext.GeneratePlatform();

                Sessions.Broadcast(JsonConvert.SerializeObject(platform));
                Console.WriteLine("-RunGame- Spawn platform:" + platform.ToString());
                await Task.Delay(1000);
            }
        }
    }
}
