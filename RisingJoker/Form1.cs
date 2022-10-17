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
        }

        bool needToStartGame, GameRunning, isWaitingForResponse;
        int score;

        //server stuff
        static readonly string serverAddress = "ws://127.0.0.1:6969";
        readonly WebSocket runSocket = new WebSocket(serverAddress + "/RunGame");
        readonly WebSocket lobbySocket = new WebSocket(serverAddress + "/JoinGame");

        //for spawning platforms
        double currentTime = 0;
        double nextSpawnTime = 1;

        Player userPlayer;

        //data objects that are received from server
        Stack<string> MenuMessages = new Stack<string>();
        PlatformDto platformToSpawnData;
        List<MovableObject> movableGameObjects = new List<MovableObject>();
        List<Platform> platformList = new List<Platform>();

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
            //consoleBoard.Text = menuBoardText;

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

            movableGameObjects.ForEach(obj => obj.Move());
            platformList.ForEach(obj =>
            {
                if (obj.IsCollidingWith(userPlayer))
                {
                    obj.OnCollision(userPlayer);
                }
            });

            movableGameObjects.ForEach(obj => obj.MoveDisplayObject());
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

        //Key press event (what happens when a certain key is being held down)
        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            SetPlayerMovement(e, true);
        }

        //Key release event (what happens when a certain key is not being pressed)
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
            MenuMessages.Push("Server: " + e.Data);
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

            MenuMessages.Push("Server: " + e.Data);
            if (e.Data == "Game Start!")
            {
                needToStartGame = true;
            }
        }

        //Method that's supposed to spawn the player when the game starts
        //---! NEEDS FIXING !--- the method currently only makes already existing player object visible 
        private void SpawnPlayer()
        {
            userPlayer = new Player(new Size(25, 25), new Point(0, 0), true, Color.Blue);
            movableGameObjects.Add(userPlayer);
            userPlayer.Render();
        }

        //Method that spawns a platform with specified parameters (received from server)

        private void SpawnPlatform(PlatformDto platformData, int yPosition)
        {
            PlatformBuilder platformBuilder =
                new PlatformBuilder()
                .SetDirectionSpeed(MoveDirection.Down, FALL_SPEED)
                .SetSize(new Size(platformData.Width, platformData.Height))
                .SetPosition(new Point(platformData.PositionX, yPosition))
                .SetColor(Color.Brown);
            if (platformData.HasCoin)
            {
                platformBuilder.AddCoin(platformData.CoinPosX);
            }
            if (platformData.HasEnemy)
            {
                platformBuilder.AddEnemy(platformData.EnemyPosX);
            }

            Platform platform = platformBuilder.GetPlatform();
            movableGameObjects.Add(platform);
            platformList.Add(platform);
            platform.Render();
        }

    }
}
