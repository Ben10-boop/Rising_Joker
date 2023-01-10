using Newtonsoft.Json;
using RisingJoker.DTOs;
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

        bool needToStartGame, GameRunning, isWaitingForResponse;
        int score;

        //server stuff
        static readonly string serverAddress = "ws://25.44.67.63:6969"; // <- This for multiplayer
        //static readonly string serverAddress = "ws://127.0.0.1:6969";
        readonly WebSocket runSocket = new WebSocket(serverAddress + "/RunGame");
        readonly WebSocket lobbySocket = new WebSocket(serverAddress + "/JoinGame");
        readonly WebSocket playerPosBroadcastSocket = new WebSocket(serverAddress + "/PlayerPosBroadcast");

        //for spawning platforms
        double currentTime = 0;
        double nextSpawnTime = 1;

        //player information
        Player userPlayer;
        PlayerColor selectedColorEnum;

        /*
        Opponent object and position lists are synchronized by index, 
        thus saving the color in position object is not needed.
         */
        List<Player> Opponents = new List<Player>();
        PlayerPositionDto[] OpponentPositions = new PlayerPositionDto[]{
            new PlayerPositionDto { PositionX = 0, PositionY = 50, PlayerColor = PlayerColor.None.ToString() },
            new PlayerPositionDto { PositionX = 0, PositionY = 50, PlayerColor = PlayerColor.None.ToString() }
        };

        //data objects that are received from server
        Stack<string> MenuMessages = new Stack<string>();
        PlatformDto platformToSpawnData;
        List<GameObject> GameObjects = new List<GameObject>();

        /*
        Game timer event that happens every game frame. Everything that influences what is seen on
        the screen needs to be called from within the scope of this method
        */
        private void GameTickEvent(object sender, EventArgs e)
        {
            if (GameRunning)
            {
                playerPosBroadcastSocket.Send(JsonConvert.SerializeObject(new PlayerPositionDto
                {
                    PlayerColor = selectedColorEnum.ToString(),
                    PositionX = userPlayer.position.X,
                    PositionY = userPlayer.position.Y
                }));
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
            scoreBoard.Text = String.Format("Score: {0}\nTime: {1:n}", score, currentTime);
            if (currentTime >= nextSpawnTime && platformToSpawnData != null)
            {
                score += 10;
                nextSpawnTime += 1;
                SpawnPlatform(platformToSpawnData, 0);
                platformToSpawnData = null;
            }

            UpdateOpponentsPositions();
            GameObjects.ForEach(obj =>
            {
                obj.Move();
                GameObjects.ForEach(otherObj =>
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

                obj.MoveObjectInDisplay();
            });
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
                else if ((string)x.Tag == "menuText" || (string)x.Tag == "menuButton")
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
            for (int i = 0; i < Opponents.Count; i++)
            {
                Opponents[i].MoveTo(new Point(OpponentPositions[i].PositionX, OpponentPositions[i].PositionY));
            }
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

            MenuMessages.Push($"Attempting to play as {color}");

            lobbySocket.Send(color.ToString());
            isWaitingForResponse = true;
        }


        private void OnStartClick(object sender, EventArgs e)
        {
            if (isWaitingForResponse)
            {
                return;
            }

            runSocket.Send("I want to start the game");
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
            if (e.KeyCode == Keys.Up)
            {
                userPlayer.SetMovement(MoveDirection.Up, isMoving);
            }
        }


        //Methods that are executed when a message is received from the server

        //Message from JoinGame service
        private void Ws_OnMessage(object sender, MessageEventArgs e)
        {
            MenuMessages.Push("Server: " + e.Data);
            isWaitingForResponse = false;
            if (e.Data.StartsWith("Joined as"))
            {
                string color = e.Data.Split('\'')[1];
                SetPlayerColor(color);
            }
        }
        //Message from RunGame service
        private void RunWs_OnMessage(object sender, MessageEventArgs e)
        {
            if (GameRunning)
            {
                platformToSpawnData = JsonConvert.DeserializeObject<PlatformDto>(e.Data);
                return;
            }

            MenuMessages.Push("Server: " + e.Data);
            if (e.Data == "Game Start!")
            {
                needToStartGame = true;
            }
        }
        //Message from PlayerPositionBroadcast service
        private void PlayerPosBroadcastWs_OnMessage(object sender, MessageEventArgs e)
        {
            PlayerPositionDto[] playersPositions = JsonConvert.DeserializeObject<PlayerPositionDto[]>(e.Data);
            switch (selectedColorEnum)
            {
                case PlayerColor.Red:
                    OpponentPositions[0] = playersPositions[1];
                    OpponentPositions[1] = playersPositions[2];
                    break;
                case PlayerColor.Green:
                    OpponentPositions[0] = playersPositions[0];
                    OpponentPositions[1] = playersPositions[2];
                    break;
                case PlayerColor.Blue:
                    OpponentPositions[0] = playersPositions[0];
                    OpponentPositions[1] = playersPositions[1];
                    break;
            }
        }

        private void SetPlayerColor(string playerColor)
        {
            if (playerColor == "Blue")
            {
                selectedColorEnum = PlayerColor.Blue;
            }
            if (playerColor == "Red")
            {
                selectedColorEnum = PlayerColor.Red;
            }
            if (playerColor == "Green")
            {
                selectedColorEnum = PlayerColor.Green;
            }
        }

        /*
        Spawns the player and the opponents. The order in which opponents get added is relevant,
        this way, the position indexes match the opponent object indexes
        */
        private void SpawnPlayer()
        {
            switch (selectedColorEnum)
            {
                case PlayerColor.Red:
                    userPlayer = new Player(new Size(25, 25), new Point(0, 0), true, Color.Red);

                    Opponents.Add(new Player(new Size(25, 25), new Point(0, 0), true, Color.Green));
                    Opponents.Add(new Player(new Size(25, 25), new Point(0, 0), true, Color.Blue));
                    break;
                case PlayerColor.Blue:
                    userPlayer = new Player(new Size(25, 25), new Point(0, 0), true, Color.Blue);

                    Opponents.Add(new Player(new Size(25, 25), new Point(0, 0), true, Color.Red));
                    Opponents.Add(new Player(new Size(25, 25), new Point(0, 0), true, Color.Green));
                    break;
                case PlayerColor.Green:
                    userPlayer = new Player(new Size(25, 25), new Point(0, 0), true, Color.Green);

                    Opponents.Add(new Player(new Size(25, 25), new Point(0, 0), true, Color.Red));
                    Opponents.Add(new Player(new Size(25, 25), new Point(0, 0), true, Color.Blue));
                    break;
            }
            GameObjects.Add(userPlayer);
            userPlayer.Render();

            Opponents.ForEach((opponent) =>
            {
                GameObjects.Add(opponent);
                opponent.Render();
            });
        }

        //Method that spawns a platform with specified parameters (received from server)
        private void SpawnPlatform(PlatformDto platformData, int yPosition)
        {
            Platform platform = new Platform(new Size(platformData.Width, platformData.Height), new Point(platformData.PositionX, yPosition), Color.Brown)
            {
                DownDirectionSpeed = FALL_SPEED
            };
            GameObjects.Add(platform);
            platform.Render();
        }

    }
}
