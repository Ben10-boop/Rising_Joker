using Newtonsoft.Json;
using RisingJoker.BaseGameObjects;
using RisingJoker.DTOs;
using RisingJoker.PlatformsBuilder;
using RisingJoker.RenderingAdapters;
using RisingJoker.Themes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using WebSocketSharp;

namespace RisingJoker
{
    public enum PlayerColor
    {
        Red = 0,
        Green = 1,
        Blue = 2,
        None = 3
    }

    public partial class Form1 : Form
    {
        private List<PlatformColorTheme> platformThemeList;
        private Menu MenuHandler;
        private RunningGame GameRunner;
        IRenderer screenRenderer;
        public Form1()
        {
            InitializeComponent();

            MenuHandler = new Menu(consoleBoard, Controls);
            screenRenderer = new MenuRenderer(MenuHandler);
            MenuHandler.InitializeMenu();

            runSocket.OnMessage += RunWs_OnMessage;
            runSocket.Connect();

            lobbySocket.OnMessage += Ws_OnMessage;
            lobbySocket.Connect();

            playerPosBroadcastSocket.OnMessage += PlayerPosBroadcastWs_OnMessage;
            playerPosBroadcastSocket.Connect();
        }


        bool needToStartGame, GameRunning, isWaitingForResponse;

        //server stuff
        //static readonly string serverAddress = "ws://25.44.67.63:6969"; // <- This for multiplayer
        static readonly string serverAddress = "ws://127.0.0.1:6969";

        readonly WebSocket runSocket = new WebSocket(serverAddress + "/RunGame");
        readonly WebSocket lobbySocket = new WebSocket(serverAddress + "/JoinGame");
        readonly WebSocket playerPosBroadcastSocket = new WebSocket(serverAddress + "/PlayerPosBroadcast");

        /*
        Game timer event that happens every game frame. Everything that influences what is seen on
        the screen needs to be called from within the scope of this method
        */
        private void GameTickEvent(object sender, EventArgs e)
        {
            if (needToStartGame) StartGame();
            screenRenderer.Render();

            if (GameRunning && !needToStartGame)
            {
                playerPosBroadcastSocket.Send(JsonConvert.SerializeObject(new PlayerPositionDto
                {
                    PlayerColor = RunningGame.UserColor.ToString(),
                    PositionX = GameRunner.UserPlayer.position.X,
                    PositionY = GameRunner.UserPlayer.position.Y
                }));
                ShowFps();
            }
        }

        private void StartGame()
        {
            GameObject.SetGameScreen(ActiveForm);

            platformThemeList = ThemesReader.ReadThemes<PlatformColorTheme>("./Themes/PlatformThemes.json");
            GameRunner = new RunningGame(scoreBoard, platformThemeList);
            screenRenderer = new RunningGameRenderer(GameRunner);
            GameRunner.SpawnPlayer();
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
                GameRunner.SpawnPlatform(platform, yPos);
            });


            GameRunning = true;
            needToStartGame = false;
        }

        //Button clicking events (what happens when a certain button is clicked)
        private void OnRedSelectButtonClick(object sender, EventArgs e)
        {
            OnColorSelectClick(PlayerColor.Red);
            ChangeButtonsAppearance(PlayerColor.Red);
        }

        private void OnGreenSelectButtonClick(object sender, EventArgs e)
        {
            OnColorSelectClick(PlayerColor.Green);
            ChangeButtonsAppearance(PlayerColor.Green);


        }
        private void OnBlueSelectButtonClick(object sender, EventArgs e)
        {
            OnColorSelectClick(PlayerColor.Blue);
            ChangeButtonsAppearance(PlayerColor.Blue);

        }
        private void OnUndoColorButtonClick(object sender, EventArgs e)
        {
            OnColorSelectClick(PlayerColor.None);
            ChangeButtonsAppearance(PlayerColor.None);
        }
        private void ChangeButtonsAppearance(PlayerColor color)
        {
            string receivedColor = color.ToString();
            switch (color)
            {
                case PlayerColor.Red:
                case PlayerColor.Blue:
                case PlayerColor.Green:
                    redSelectButton.Visible = false;
                    blueSelectButton.Visible = false;
                    greenSelectButton.Visible = false;
                    undoColorButton.Visible = true;
                    undoColorButton.ForeColor = Color.FromName(receivedColor);
                    break;
                case PlayerColor.None:
                    redSelectButton.Visible = true;
                    blueSelectButton.Visible = true;
                    greenSelectButton.Visible = true;
                    undoColorButton.Visible = false;
                    break;
                default: break;
            }
        }

        private void OnColorSelectClick(PlayerColor color)
        {
            if (isWaitingForResponse)
            {
                return;
            }

            if (color == PlayerColor.None)
            {
                MenuHandler.AddNewMessage($"Undoing color selection");
                lobbySocket.Send(JsonConvert.SerializeObject(new StringDto { Value = color.ToString() }));
                RunningGame.UserColor = PlayerColor.None;
                return;
            }
            else
            {
                MenuHandler.AddNewMessage($"Attempting to play as {color}");
                lobbySocket.Send(JsonConvert.SerializeObject(new StringDto { Value = color.ToString() }));
            }
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
                GameRunner.UserPlayer.SetMovement(MoveDirection.Left, isMoving);
            }
            if (e.KeyCode == Keys.Right)
            {
                GameRunner.UserPlayer.SetMovement(MoveDirection.Right, isMoving);
            }
            if (e.KeyCode == Keys.Up)
            {
                GameRunner.UserPlayer.SetMovement(MoveDirection.Up, isMoving);
            }
        }


        //Methods that are executed when a message is received from the server
        //Message from JoinGame service
        private void Ws_OnMessage(object sender, MessageEventArgs e)
        {
            Console.WriteLine(e.Data);
            StringDto joinResult = JsonConvert.DeserializeObject<StringDto>(e.Data);
            switch (joinResult.Value)
            {
                case "Joined as Blue":
                    RunningGame.UserColor = PlayerColor.Blue;
                    break;
                case "Joined as Red":
                    RunningGame.UserColor = PlayerColor.Red;
                    break;
                case "Joined as Green":
                    RunningGame.UserColor = PlayerColor.Green;
                    break;
                default:
                    //Color was already taken
                    break;
            }
            MenuHandler.AddNewMessage("Server: " + joinResult.Value);
            isWaitingForResponse = false;
        }
        //Message from RunGame service
        private void RunWs_OnMessage(object sender, MessageEventArgs e)
        {
            if (GameRunning)
            {
                GameRunner.PlatformToSpawnData = JsonConvert.DeserializeObject<PlatformDto>(e.Data);
                return;
            }

            StringDto message = JsonConvert.DeserializeObject<StringDto>(e.Data);
            MenuHandler.AddNewMessage("Server: " + message.Value);
            if (message.Value == "Game Start!")
            {
                needToStartGame = true;
            }
        }
        private void PlayerPosBroadcastWs_OnMessage(object sender, MessageEventArgs e)
        {
            PlayerPositionDto[] playersPositions = JsonConvert.DeserializeObject<PlayerPositionDto[]>(e.Data);
            switch (RunningGame.UserColor)
            {
                case PlayerColor.Red:
                    GameRunner.OpponentPositions[0] = playersPositions[1];
                    GameRunner.OpponentPositions[1] = playersPositions[2];
                    break;
                case PlayerColor.Green:
                    GameRunner.OpponentPositions[0] = playersPositions[0];
                    GameRunner.OpponentPositions[1] = playersPositions[2];
                    break;
                case PlayerColor.Blue:
                    GameRunner.OpponentPositions[0] = playersPositions[0];
                    GameRunner.OpponentPositions[1] = playersPositions[1];
                    break;
            }
        }

        private int lastTick;
        private int lastFrameRate;
        private int frameRate;
        private int minFps = int.MaxValue;
        private int maxFps;
        private int totalFps;
        private int timePassed;
        private PerformanceCounter performanceCounter = new PerformanceCounter
        {
            CategoryName = "Process",

            CounterName = "Working Set",

            InstanceName = Process.GetCurrentProcess().ProcessName
        };
        private long kbUsed;

        private Tuple<int, int, int, int, long> CalculateFrameRate()
        {

            if (System.Environment.TickCount - lastTick >= 1000)
            {
                kbUsed = GC.GetTotalMemory(true) / 1024;
                lastFrameRate = frameRate;
                totalFps += lastFrameRate;
                timePassed++;

                if (lastFrameRate > maxFps && timePassed > 1)
                {
                    maxFps = lastFrameRate;
                }
                if (lastFrameRate < minFps && timePassed > 1)
                {
                    minFps = lastFrameRate;
                }
                frameRate = 0;
                lastTick = System.Environment.TickCount;
            }
            frameRate++;
            return Tuple.Create(lastFrameRate, minFps == int.MaxValue ? 0 : minFps, maxFps, timePassed < 2 ? lastFrameRate : totalFps / (timePassed - 1), kbUsed);
        }

        private void ShowFps()
        {
            var fpsVals = CalculateFrameRate();

            fps.Text = $"FPS: {fpsVals.Item1}\r\nMin: {fpsVals.Item2}\r\nMax: {fpsVals.Item3}\r\nAvg: {fpsVals.Item4} \r\nRAM: {fpsVals.Item5}KB";
        }

    }
}
