using RisingJoker.BaseGameObjects;
using RisingJoker.CoinObject;
using RisingJoker.DTOs;
using RisingJoker.PlatformFactory;
using RisingJoker.PlatformsBuilder;
using RisingJoker.PlayerFactoryMethod;
using RisingJoker.PointsObserver;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace RisingJoker.RenderingAdapters
{
    public class RunningGame
    {
        private const int FALL_SPEED = 2;
        private int PreviousLevel = -1;
        double CurrentTime = 0;
        double NextSpawnTime = 1;
        private Dictionary<PlatFactoryType, Coin> CoinMap;
        private Dictionary<PlatFactoryType, IEnemy> EnemyMap;
        private Dictionary<Color, PointsCollector> PointsCollectorMap;
        List<PlatformColorTheme> PlatformThemeList;
        public static PlayerColor UserColor = PlayerColor.None;
        Label ScoreBoard;
        List<IGameObject> GameObjects;
        public List<Player> Opponents;
        public Player UserPlayer;
        public PlayerPositionDto[] OpponentPositions;

        public PlatformDto PlatformToSpawnData;

        public RunningGame(Label scoreBoard, List<PlatformColorTheme> platformThemeList)
        {
            ScoreBoard = scoreBoard;
            GameObjects = new List<IGameObject>();
            PlatformThemeList = platformThemeList;
            CoinMap = new Dictionary<PlatFactoryType, Coin>();
            EnemyMap = new Dictionary<PlatFactoryType, IEnemy>();
            PointsCollectorMap = new Dictionary<Color, PointsCollector>();
            Opponents = new List<Player>();
            OpponentPositions = new PlayerPositionDto[]
        {
            new PlayerPositionDto { PositionX = 0, PositionY = 50, PlayerColor = PlayerColor.None.ToString() },
            new PlayerPositionDto { PositionX = 0, PositionY = 50, PlayerColor = PlayerColor.None.ToString() }
        };
        }

        public void BeforePhysics()
        {
            var ranNum = new Random().Next();
            CurrentTime += 0.02;
            char[] trimChars = new char[] { '[', ']' };
            ScoreBoard.Text = String.Format("Score: \n {0}: {1}", UserPlayer.color.Name, PointsCollectorMap[UserPlayer.color].Points);
            Opponents.ForEach((opponent) =>
            {
                ScoreBoard.Text += String.Format("\n {0}: {1}", opponent.color.Name, PointsCollectorMap[opponent.color].Points);
            });
            ScoreBoard.Text += String.Format("\n Time: {0:n}", CurrentTime);
            if (CurrentTime >= NextSpawnTime && PlatformToSpawnData != null)
            {
                NextSpawnTime += 1;
                SpawnPlatform(PlatformToSpawnData, 0);
                PlatformToSpawnData = null;
            }
            if (UserPlayer != null)
                UserPlayer.UpdateUniqueMechanicPoints(CurrentTime);
            UpdateOpponentsPositions();
        }

        public void SpawnPlayer()
        {
            RedPlayerCreator redCreator = new RedPlayerCreator();
            GreenPlayerCreator greenCreator = new GreenPlayerCreator();
            BluePlayerCreator blueCreator = new BluePlayerCreator();
            switch (UserColor)
            {
                case PlayerColor.Red:
                    UserPlayer = redCreator.CreatePlayer();

                    Opponents.Add(greenCreator.CreatePlayer());
                    Opponents.Add(blueCreator.CreatePlayer());
                    break;
                case PlayerColor.Blue:
                    UserPlayer = blueCreator.CreatePlayer();

                    Opponents.Add(redCreator.CreatePlayer());
                    Opponents.Add(greenCreator.CreatePlayer());
                    break;
                case PlayerColor.Green:
                    UserPlayer = greenCreator.CreatePlayer();

                    Opponents.Add(redCreator.CreatePlayer());
                    Opponents.Add(blueCreator.CreatePlayer());
                    break;
                default:
                    RedPlayerCreator spectatorCreator = new RedPlayerCreator();
                    UserPlayer = spectatorCreator.CreatePlayer();
                    break;

            }
            List<Player> createdPlayers = new List<Player>();
            createdPlayers.Add(UserPlayer);
            GameObjects.Add(UserPlayer);
            Opponents.ForEach((opponent) =>
            {
                createdPlayers.Add(opponent);
                GameObjects.Add(opponent);
            });
            createdPlayers.ForEach((player) =>
            {
                PointsCollector pointsCollector = new PointsCollector(player.color.ToString());
                player.Subscribe(pointsCollector);
                PointsCollectorMap.Add(player.color, pointsCollector);
            });
        }

        public void RenderPhysics()
        {
            List<IGameObject> objectsToRemove = new List<IGameObject>();
            for (int gameObjectIndex = 0; gameObjectIndex < GameObjects.Count; gameObjectIndex++)
            {
                var obj = GameObjects[gameObjectIndex];
                IMovableObject movableObject = obj is IMovableObject @object ? @object : null;
                if (movableObject != null)
                {
                    movableObject.Move();
                }
                for (int i = gameObjectIndex; i < GameObjects.Count; i++)
                {

                    var otherObj = GameObjects[i];
                    try
                    {
                        if (obj.IsCollidingWith(otherObj))
                        {
                            obj.OnCollisionWith(otherObj);
                            otherObj.OnCollisionWith(obj);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(obj.objectTag);
                    }
                }
                if (!obj.IsObjectAlive())
                {
                    objectsToRemove.Add(obj);
                }
                obj.Render();
            };
            objectsToRemove.ForEach((o) =>
            {
                o.RemoveFromScreen();
                GameObjects.Remove(o);

            });
        }

        private void UpdateOpponentsPositions()
        {
            for (int i = 0; i < Opponents.Count; i++)
            {
                Opponents[i].MoveTo(new Point(OpponentPositions[i].PositionX, OpponentPositions[i].PositionY));
            }
        }

        public void SpawnPlatform(PlatformDto platformData, int yPosition)
        {
            PlatformColorTheme platTheme = PlatformThemeList[(platformData.Level - 1) % 2];
            PlatformObjPickFacade platformObjPickFacade = new PlatformObjPickFacade();

            var (platFactory, platFactoryType) = platformObjPickFacade.PickPlatform(platformData.HasCoin, platformData.HasEnemy, platTheme, platformData.CoinPosX, platformData.EnemyPosX);

            if (platformData.Level != PreviousLevel || !CoinMap.ContainsKey(platFactoryType) || !EnemyMap.ContainsKey(platFactoryType))
            {
                CoinMap[platFactoryType] = platFactory.CreateCoin(Math.Max(25 - 3 * platformData.Level, 5), (int)Math.Log(100 * Math.Pow(platformData.Level, 2)));
                EnemyMap[platFactoryType] = platFactory.CreateEnemy(new Size(Math.Max(25 - 5 * platformData.Level, 5), 25), -(int)Math.Log(Math.Pow(platformData.Level, 2)));
                PreviousLevel = platformData.Level;
            }


            IEnemy enemy = EnemyMap[platFactoryType];
            Coin coin = CoinMap[platFactoryType];

            IPlatformBuilder platformBuilder = new PlatformBuilder();
            bool shouldAddPlatformBottom = platformData.PlatformAmount == 1;

            PlatformObjAddFacade facade = new PlatformObjAddFacade(GameObjects, platformBuilder, coin, enemy, platFactory, platformData.Level, platformData.Width, PointsCollectorMap);

            for (int platformIndex = 0; platformIndex < platformData.PlatformAmount; platformIndex++)
            {
                var xOffset = platformData.NextPlatformOffset * platformIndex;
                platformBuilder
                .SetDirectionSpeed(MoveDirection.Down, FALL_SPEED)
                .SetSize(new Size(platformData.Width, platformData.Height))
                .SetPosition(new Point(platformData.PositionX + xOffset, yPosition))
                .SetColor(Color.Brown);

                if (shouldAddPlatformBottom)
                {
                    facade.AddObject(platformData.PositionX + xOffset, PlatformObjectType.platformBottom);
                }

                //adding coin and enemy if needed
                if (platformData.HasCoin)
                {
                    facade.AddObject(platformData.CoinPosX, PlatformObjectType.coin);
                }
                if (platformData.HasEnemy)
                {
                    facade.AddObject(platformData.EnemyPosX, PlatformObjectType.enemy);
                }
                platformBuilder.SetColor(platTheme.MainColor);
                Platform platform = platformBuilder.GetPlatform();
                GameObjects.Add(platform);
                platformBuilder.Reset();
            }
        }
    }
}
