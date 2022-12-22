using Newtonsoft.Json;
using RisingJokerServer.DTOs;
using RisingJokerServer.Iterator;
using RisingJokerServer.PlatGenerationStrategy;
using RisingJokerServer.PlatGenTemplateMethod;
using RisingJokerServer.PlatormVisitor;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace RisingJokerServer
{

    //handles running the game
    public class LiveGameSocket : WebSocketBehavior
    {
        private static bool gameRunning = false;
        public static bool useReverseIterator = false;
        private static IIterableCollection generationTimes = new IterableList();
        private Stopwatch watch = new Stopwatch();

        public static bool GetGameRunning()
        {
            return gameRunning;
        }

        /// <summary>
        /// Overrides the current generationTimes collection with the given one. Can be any IIterableCollection
        /// </summary>
        /// <param name="collection"></param>
        internal static void OverrideGenTimes(IIterableCollection collection)
        {
            generationTimes = collection;
        }
        public static void PrintGenerationTimerResults()
        {
            IIterator iterator;
            if (useReverseIterator)
                iterator = (generationTimes as IterableList).CreateReverseIterator();
            else
                iterator = generationTimes.CreateIterator();

            int i = 0;
            while (iterator.HasMore())
            {
                Console.WriteLine($"Platform {i} generated in {iterator.GetNext()} miliseconds");
                i++;
            }
        }

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

            //USE ONE OR THE OTHER, NOT BOTH
            //GeneratePlatWithStrat();
            GeneratePlatWithTemplate();
        }

        private async void GeneratePlatWithTemplate()
        {
            PlatformGenerator platGenerator;
            Random rand = new Random();
            IVisitor platVisitor = new VisitorShort();

            //level_1
            for (int i = 0; i < 10; i++)
            {
                if (rand.Next(0, 10) < 7)
                {
                    platGenerator = new Lvl1PlatGenerator();
                }
                else
                {
                    platGenerator = new Lvl1ArrayGenerator();
                }
                PlatformDto platform = platGenerator.GeneratePlatform();

                Sessions.Broadcast(JsonConvert.SerializeObject(platform));
                //Console.WriteLine("-RunGame- Spawn platform:" + platform.ToString());
                Console.Write("-RunGame- Spawn "); platVisitor.visitPlatform(platform);
                await Task.Delay(1000);
            }

            //level_2
            for (int i = 0; i < 40; i++)
            {
                //int rolledNum = rand.Next(0, 11);
                int rolledNum = 7;
                if (rolledNum < 6) // <- 6
                {
                    platGenerator = new Lvl2PlatGenerator();
                }
                else if (rolledNum < 9) // <- 9
                {
                    platGenerator = new Lvl2ArrayGenerator();
                }
                else
                {
                    watch.Start();
                    platGenerator = new Lvl2SpecialArrayGenerator();
                    watch.Stop();
                    generationTimes.Add(watch.ElapsedMilliseconds);
                    watch.Restart();

                }
                PlatformDto platform = platGenerator.GeneratePlatform();

                Sessions.Broadcast(JsonConvert.SerializeObject(platform));
                //Console.WriteLine("-RunGame- Spawn platform:" + platform.ToString());
                Console.Write("-RunGame- Spawn "); platVisitor.visitPlatform(platform);
                await Task.Delay(1000);
            }
        }

        private async void GeneratePlatWithStrat()
        {
            PlatGenerationContext platGenContext = new PlatGenerationContext();
            Random rand = new Random();

            IPlatGenerationStrategy platformStrategy = new Lvl1PlatformStrategy();
            IPlatGenerationStrategy arrayStrategy = new Lvl1ArrayStrategy();

            //level_1
            for (int i = 0; i < 10; i++)
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
