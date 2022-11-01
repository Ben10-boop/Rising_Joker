﻿using Newtonsoft.Json;
using RisingJoker.DTOs;
using RisingJoker.PlatformFactory;
using RisingJoker.PlatformFactory.Lvl1;
using RisingJoker.PlatformFactory.Lvl2;
using RisingJoker.PlayerFactoryMethod;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using WebSocketSharp;

namespace RisingJoker
{
    enum PlayerColor
    {
        Red = 0,
        Green = 1,
        Blue = 2,
        None = 3
    }

    public partial class Form1 : Form
    {
        private const int FALL_SPEED = 2;
        public Form1()
        {
            InitializeComponent();
            runSocket.OnMessage += RunWs_OnMessage;
            runSocket.Connect();

            lobbySocket.OnMessage += Ws_OnMessage;
            lobbySocket.Connect();

            playerPosBroadcastSocket.OnMessage += PlayerPosBroadcastWs_OnMessage;
            playerPosBroadcastSocket.Connect();
        }

        bool addedOnce = false;

        bool needToStartGame, GameRunning, isWaitingForResponse;

        List<GameObject> gameObjects = new List<GameObject>();

        //server stuff
        static readonly string serverAddress = "ws://127.0.0.1:6969";
        readonly WebSocket runSocket = new WebSocket(serverAddress + "/RunGame");
        readonly WebSocket lobbySocket = new WebSocket(serverAddress + "/JoinGame");
        readonly WebSocket playerPosBroadcastSocket = new WebSocket(serverAddress + "/PlayerPosBroadcast");

        //for spawning platforms
        double currentTime = 0;
        double nextSpawnTime = 1;
        int currentLevel = 1;

        //to keep track of Players data
        PlayerColor userColor = PlayerColor.None;
        Player userPlayer;
        List<Player> opponents = new List<Player>();

        //data objects that are received from server
        Stack<string> MenuMessages = new Stack<string>();
        PlatformDto platformToSpawnData;
        PlayerPositionDto[] opponentPositions = new PlayerPositionDto[]
        {
            new PlayerPositionDto { PositionX = 0, PositionY = 0 },
            new PlayerPositionDto { PositionX = 0, PositionY = 0 }
        };

        /*
        Game timer event that happens every game frame. Everything that influences what is seen on
        the screen needs to be called from within the scope of this method
        */
        private void GameTickEvent(object sender, EventArgs e)
        {
            if (GameRunning)
            {
                RunGame();
                return;
            }

            ShowMenu();
            if (needToStartGame) StartGame();
        }
        private void ShowMenu()
        {
            string menuBoardText = "";
            int i = 0;
            foreach (string message in MenuMessages)
            {
                if (i > 8) break;
                menuBoardText = menuBoardText + "\n" + message;
                i++;
            }
            consoleBoard.Text = menuBoardText;

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {
                    if ((string)x.Tag == "pBottom" || (string)x.Tag == "platform")
                    {
                        x.Visible = false;
                    }
                }
            }
        }

        private void RunGame()
        {
            currentTime += 0.02;
            scoreBoard.Text = String.Format("Score: {0}\nTime: {1:n}", userPlayer.GetScore(), currentTime);
            if (currentTime >= nextSpawnTime && platformToSpawnData != null)
            {
                userPlayer.ModifyScore(10);
                nextSpawnTime += 1;
                SpawnPlatform(platformToSpawnData, 0);
                platformToSpawnData = null;
            }
            userPlayer.UpdateUniqueMechanicPoints(currentTime);
            gameObjects.ForEach(obj =>
            {
                MovableObject movableObject = obj is MovableObject @object ? @object : null;
                if (movableObject != null)
                {
                    movableObject.Move();
                }

                gameObjects.ForEach(otherObj =>
                {
                    if (otherObj == obj)
                    {
                        return;
                    }

                    if (obj.IsCollidingWith(otherObj))
                    {
                        obj.OnCollisionWith(otherObj);
                    }
                });

                if (movableObject != null)
                {
                    movableObject.MoveDisplayObject();
                }
            });
            UpdateOpponentsPositions();
            playerPosBroadcastSocket.Send(JsonConvert.SerializeObject(new PlayerPositionDto
            {
                PlayerColor = userColor.ToString(),
                PositionX = userPlayer.position.X,
                PositionY = userPlayer.position.Y
            }));
        }

        private void StartGame()
        {
            GameObject.SetGameScreen(ActiveForm);

            SpawnPlayer();
            foreach (Control x in this.Controls)
            {
                if (x.Name == "scoreBoard")
                {
                    x.Visible = true;
                    x.SendToBack();
                }
                //else if ((string)x.Tag == "menuText" || (string)x.Tag == "menuButton")
                else if ((string)x.Tag == "menuButton")
                {
                    x.Visible = false;
                    x.Enabled = false;
                }
            }
            List<PlatformDto> platforms = new List<PlatformDto>() { new PlatformDto
            {
                CoinPosX = 0,
                HasCoin = false,
                HasEnemy = false,
                Height = 20,
                Width = 450,
                PositionX = 25,
                EnemyPosX = 0,
            },new PlatformDto
            {
                CoinPosX = 0,
                HasCoin = false,
                HasEnemy = false,
                Height = 20,
                Width = 400,
                PositionX = 50,
                EnemyPosX = 0,
            },
            new PlatformDto
            {
                CoinPosX = 0,
                HasCoin = false,
                HasEnemy = false,
                Height = 20,
                Width = 350,
                PositionX = 75,
                EnemyPosX = 0,
            },
            new PlatformDto
            {
                CoinPosX = 0,
                HasCoin = false,
                HasEnemy = false,
                Height = 20,
                Width = 300,
                PositionX = 25,
                EnemyPosX = 0,
            }
            };

            platforms.ForEach(platform =>
            {
                int index = platforms.IndexOf(platform);
                int yPos = 400 - (index * 100);
                SpawnPlatform(platform, yPos);
            });


            GameRunning = true;
        }

        private void UpdateOpponentsPositions()
        {
            opponents[0].position = new Point(opponentPositions[0].PositionX, opponentPositions[0].PositionY);
            opponents[1].position = new Point(opponentPositions[1].PositionX, opponentPositions[1].PositionY);
        }

        //Button clicking events (what happens when a certain button is clicked)
        private void OnRedSelectButtonClick(object sender, EventArgs e)
        {
            OnColorSelectClick(PlayerColor.Red);
        }

        private void OnGreenSelectButtonClick(object sender, EventArgs e)
        {
            OnColorSelectClick(PlayerColor.Green);

        }
        private void OnBlueSelectButtonClick(object sender, EventArgs e)
        {
            OnColorSelectClick(PlayerColor.Blue);
        }

        private void OnColorSelectClick(PlayerColor color)
        {
            if (isWaitingForResponse)
            {
                return;
            }

            if (userColor != PlayerColor.None)
            {
                MenuMessages.Push($"You have already chosen to play as {userColor}");
                return;
            }

            MenuMessages.Push($"Attempting to play as {color}");

            lobbySocket.Send(JsonConvert.SerializeObject(new StringDto { Value = color.ToString() }));
            isWaitingForResponse = true;
        }


        private void OnStartClick(object sender, EventArgs e)
        {
            if (isWaitingForResponse)
            {
                return;
            }
            runSocket.Send(JsonConvert.SerializeObject(new StringDto { Value = "I want to start the game" }));
            //runSocket.Send("I want to start the game");
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            SetPlayerMovement(e, true);
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            SetPlayerMovement(e, false);
        }

        private void SetPlayerMovement(KeyEventArgs e, bool isMoving)
        {
            if (e.KeyCode == Keys.Left)
            {
                userPlayer.SetMovement(MoveDirection.Left, isMoving);
            }
            if (e.KeyCode == Keys.Right)
            {
                userPlayer.SetMovement(MoveDirection.Right, isMoving);
            }
            if (e.KeyCode == Keys.Space)
            {
                userPlayer.SetMovement(MoveDirection.Up, isMoving);
            }
        }


        //Methods that are executed when a message is received from the server
        //Message from JoinGame service
        private void Ws_OnMessage(object sender, MessageEventArgs e)
        {
            StringDto joinResult = JsonConvert.DeserializeObject<StringDto>(e.Data);
            switch (joinResult.Value)
            {
                case "Joined as Blue":
                    userColor = PlayerColor.Blue;
                    break;
                case "Joined as Red":
                    userColor = PlayerColor.Red;
                    break;
                case "Joined as Green":
                    userColor = PlayerColor.Green;
                    break;
                default:
                    //Color was already taken
                    break;
            }
            MenuMessages.Push("Server: " + joinResult.Value);
            isWaitingForResponse = false;
        }
        //Message from RunGame service
        private void RunWs_OnMessage(object sender, MessageEventArgs e)
        {
            if (GameRunning)
            {
                platformToSpawnData = JsonConvert.DeserializeObject<PlatformDto>(e.Data);
                return;
            }

            StringDto message = JsonConvert.DeserializeObject<StringDto>(e.Data);
            MenuMessages.Push("Server: " + message.Value);
            if (message.Value == "Game Start!")
            {
                needToStartGame = true;
            }
        }
        private void PlayerPosBroadcastWs_OnMessage(object sender, MessageEventArgs e)
        {
            PlayerPositionDto[] playersPositions = JsonConvert.DeserializeObject<PlayerPositionDto[]>(e.Data);
            switch (userColor)
            {
                case PlayerColor.Red:
                    opponentPositions[0] = playersPositions[1];
                    opponentPositions[1] = playersPositions[2];
                    break;
                case PlayerColor.Green:
                    opponentPositions[0] = playersPositions[0];
                    opponentPositions[1] = playersPositions[2];
                    break;
                case PlayerColor.Blue:
                    opponentPositions[0] = playersPositions[0];
                    opponentPositions[1] = playersPositions[1];
                    break;
            }
        }

        private void SpawnPlayer()
        {
            RedPlayerCreator redCreator = new RedPlayerCreator();
            GreenPlayerCreator greenCreator = new GreenPlayerCreator();
            BluePlayerCreator blueCreator = new BluePlayerCreator();
            switch (userColor)
            {
                case PlayerColor.Red:
                    userPlayer = redCreator.CreatePlayer();

                    opponents.Add(greenCreator.CreatePlayer());
                    opponents.Add(blueCreator.CreatePlayer());
                    break;
                case PlayerColor.Blue:
                    userPlayer = blueCreator.CreatePlayer();

                    opponents.Add(redCreator.CreatePlayer());
                    opponents.Add(greenCreator.CreatePlayer());
                    break;
                case PlayerColor.Green:
                    userPlayer = greenCreator.CreatePlayer();

                    opponents.Add(redCreator.CreatePlayer());
                    opponents.Add(blueCreator.CreatePlayer());
                    break;
                default:
                    RedPlayerCreator spectatorCreator = new RedPlayerCreator();
                    userPlayer = spectatorCreator.CreatePlayer();
                    break;

            }
            gameObjects.Add(userPlayer);
            userPlayer.Render();
        }

        //Method that spawns a platform with specified parameters (received from server)

        private void SpawnPlatform(PlatformDto platformData, int yPosition)
        {
            if (platformData.Level == 2)
                currentLevel = 2;

            IPlatFactory platFactory;
            if (currentLevel == 1)
                platFactory = new Lvl1PlatFactory();
            else
                platFactory = new Lvl2PlatFactory();

            PlatformBuilder platformBuilder =
                new PlatformBuilder()
                .SetDirectionSpeed(MoveDirection.Down, FALL_SPEED)
                .SetSize(new Size(platformData.Width, platformData.Height))
                .SetPosition(new Point(platformData.PositionX, yPosition))
                .SetColor(Color.Brown);
            PlatformBottom bottom = platFactory.CreatePlatformBottom(platformData.Width, platformData.PositionX);
            platformBuilder.AddBottom(bottom, consoleBoard);
            if (platformData.HasCoin && !addedOnce)
            {
                Coin coin = platFactory.CreateCoin(platformData.CoinPosX);
                gameObjects.Add(coin);
                platformBuilder.AddCoin(coin, consoleBoard);
            }
            if (platformData.HasEnemy && !addedOnce)
            {
                Enemy enemy = platFactory.CreateEnemy(platformData.EnemyPosX);
                gameObjects.Add(enemy);
                platformBuilder.AddEnemy(enemy, consoleBoard);
            }

            Platform platform = platformBuilder.GetPlatform();
            gameObjects.Add(platform);
            platform.Render();
        }

    }
}
