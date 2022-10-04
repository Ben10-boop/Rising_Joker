using Newtonsoft.Json;
using RisingJoker.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebSocketSharp;

namespace RisingJoker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        bool playerGoingLeft, playerGoingRight, playerJumping, needToStartGame, GameRunning, wait;
        int score;

        //server stuff
        static string serverAddress = "ws://127.0.0.1:6969";
        WebSocket runSocket = new WebSocket(serverAddress + "/RunGame");

        //for spawning platforms
        double currentTime = 0;
        double nextSpawnTime = 1;

        //jump stuff
        int force;
        double jumpCooldown;

        //movement speeds
        int playerVertSpeed;
        int playerHorSpeed = 7;

        int platformVertSpeed = 2;
        int platformHorSpeed = 3;

        //data objects that are received from server
        Stack<string> MenuMessages = new Stack<string>();
        PlatformDto platformToSpawnData = new PlatformDto();

        /*
        Game timer event that happens every game frame. Everything that influences what is seen on
        the screen needs to be called from within the scope of this method
        */
        private void GameTickEvent(object sender, EventArgs e)
        {
            if (GameRunning)
            {
                RunGame();
            }
            else
            {
                ShowMenu();
                if (needToStartGame) doGameStartSequence();
            }
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
            jumpCooldown -= 0.02;
            scoreBoard.Text = String.Format("Score: {0}\nTime: {1:n}", score, currentTime);
            
            if (currentTime >= nextSpawnTime)
            {
                score += 10;
                nextSpawnTime += 1;
                SpawnPlatform(platformToSpawnData);
            }

            player.Top += playerVertSpeed;

            if (playerGoingLeft == true)
            {
                player.Left -= playerHorSpeed;
            }
            if (playerGoingRight == true)
            {
                player.Left += playerHorSpeed;
            }

            if (playerJumping == true && force < 0)
            {
                playerJumping = false;
                jumpCooldown = 0.6;
            }
            if (playerJumping == true && jumpCooldown < 0)
            {
                playerVertSpeed = -8;
                force--;
            }
            else
            {
                playerVertSpeed = 5;
            }

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {
                    if ((string)x.Tag == "pBottom")
                    {
                        if (player.Bounds.IntersectsWith(x.Bounds))
                        {
                            force = -1;
                            jumpCooldown = 0.4;
                            player.Top = x.Top + x.Height + 2;
                            score -= 50;
                        }
                    }
                    else if ((string)x.Tag == "platform")
                    {
                        if (player.Bounds.IntersectsWith(x.Bounds))
                        {
                            force = 8;
                            player.Top = x.Top - player.Height;
                        }
                    }

                    if ((string)x.Tag == "pBottom" || (string)x.Tag == "platform")
                    {
                        //x.Visible = true;
                        x.BringToFront();
                        x.Top += platformVertSpeed;
                    }

                    if ((string)x.Tag == "coin")
                    {
                        x.Top += platformVertSpeed;
                        if (player.Bounds.IntersectsWith(x.Bounds) && x.Visible)
                        {
                            x.Visible = false;
                            x.Enabled = false;
                            score += 50;
                        }
                    }
                    if ((string)x.Tag == "enemy")
                    {
                        x.Top += platformVertSpeed;
                        if (player.Bounds.IntersectsWith(x.Bounds))
                        {
                            score -= 5;
                        }
                    }
                }
            }
        }

        private void doGameStartSequence()
        {
            SpawnPlayer();
            foreach (Control x in this.Controls)
            {
                if (x.Name == "scoreBoard")
                {
                    x.Visible = true;
                    x.SendToBack();
                }
                else if ((string)x.Tag == "platform")
                {
                    x.Visible = true;
                }
                else if ((string)x.Tag == "menuText" || (string)x.Tag == "menuButton")
                {
                    x.Visible = false;
                    x.Enabled = false;
                }
            }
            GameRunning = true;
        }

        //Button clicking events (what happens when a certain button is clicked)
        private void redSelectButton_Click(object sender, EventArgs e)
        {
            MenuMessages.Push("Attempting to play as red");
            using (WebSocket ws = new WebSocket(serverAddress + "/JoinGame"))
            {
                ws.OnMessage += Ws_OnMessage;

                ws.Connect();
                ws.Send("I want to be red");

                wait = true;
                while (wait)
                {
                    // =D
                }
            }
        }

        private void greenSelectButton_Click(object sender, EventArgs e)
        {
            MenuMessages.Push("Attempting to play as green");
            using (WebSocket ws = new WebSocket(serverAddress + "/JoinGame"))
            {
                ws.OnMessage += Ws_OnMessage;

                ws.Connect();
                ws.Send("I want to be green");

                wait = true;
                while (wait)
                {

                }
            }
        }
        private void blueSelectButton_Click(object sender, EventArgs e)
        {
            MenuMessages.Push("Attempting to play as blue");
            using (WebSocket ws = new WebSocket(serverAddress + "/JoinGame"))
            {
                ws.OnMessage += Ws_OnMessage;

                ws.Connect();
                ws.Send("I want to be blue");

                wait = true;
                while (wait)
                {
                    
                }
            }
        }
        private void startButton_Click(object sender, EventArgs e)
        {
            runSocket.OnMessage += RunWs_OnMessage;
            runSocket.Connect();
            runSocket.Send("I want to start the game");
        }

        //Key press event (what happens when a certain key is being held down)
        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Left)
            {
                playerGoingLeft = true;
            }
            if (e.KeyCode == Keys.Right)
            {
                playerGoingRight = true;
            }
            if (e.KeyCode == Keys.Space)
            {
                playerJumping = true;
            }

        }

        //Key release event (what happens when a certain key is not being pressed)
        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                playerGoingLeft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                playerGoingRight = false;
            }
            if (e.KeyCode == Keys.Space)
            {
                playerJumping = false;
            }


        }

        //Methods that are executed when a message is received from the server
        //Message from JoinGame service
        private void Ws_OnMessage(object sender, MessageEventArgs e)
        {
            MenuMessages.Push("Server: " + e.Data);
            wait = false;
        }
        //Message from RunGame service
        private void RunWs_OnMessage(object sender, MessageEventArgs e)
        {
            if (!GameRunning)
            {
                MenuMessages.Push("Server: " + e.Data);
                if(e.Data == "Game Start!")
                {
                    needToStartGame = true;
                }
            }
            else
            {
                platformToSpawnData = JsonConvert.DeserializeObject<PlatformDto>(e.Data);
            }
        }

        //Method that's supposed to spawn the player when the game starts
        //---! NEEDS FIXING !--- the method currently only makes already existing player object visible 
        private void SpawnPlayer()
        {
            foreach(Control x in this.Controls)
            {
                if(x is PictureBox && (string)x.Tag == "player")
                {
                    x.Visible = true;
                }
            }
        }

        //Method that spawns a platform with specified parameters (received from server)
        private void SpawnPlatform(PlatformDto platformData)
        {
            /*
            var rand = new Random();
            int platformSize = rand.Next(200, 300);
            int platformPosition = rand.Next(0, 500 - platformSize);*/

            PictureBox newPlatform = new PictureBox();
            newPlatform.BackColor = Color.Brown;
            newPlatform.Size = new Size(platformData.Width, platformData.Height);
            newPlatform.Location = new Point(platformData.PositionX, 0); //0, 0 - 500, 0
            newPlatform.Visible = true;
            newPlatform.Tag = "platform";

            PictureBox newPbottom = new PictureBox();
            newPbottom.BackColor = Color.Brown;
            newPbottom.Size = new Size(platformData.Width - 40, 10);
            newPbottom.Location = new Point(platformData.PositionX + 20, 15);
            newPbottom.Visible = true;
            newPbottom.Tag = "pBottom";


            ActiveForm.Controls.Add(newPlatform);
            ActiveForm.Controls.Add(newPbottom);

            if (platformData.HasCoin) SpawnCoin(platformData);
            if (platformData.HasEnemy) SpawnEnemy(platformData);
        }

        private void SpawnCoin(PlatformDto platformData)
        {
            PictureBox newCoin = new PictureBox();
            newCoin.BackColor = Color.Yellow;
            newCoin.Size = new Size(25, 25);
            newCoin.Location = new Point(platformData.PositionX + platformData.CoinPosX, -25); //0, 0 - 500, 0
            newCoin.Visible = true;
            newCoin.Tag = "coin";

            ActiveForm.Controls.Add(newCoin);
        }
        private void SpawnEnemy(PlatformDto platformData)
        {
            PictureBox newEnemy = new PictureBox();
            newEnemy.BackColor = Color.Purple;
            newEnemy.Size = new Size(25, 15);
            newEnemy.Location = new Point(platformData.PositionX + platformData.EnemyPosX, -15); //0, 0 - 500, 0
            newEnemy.Visible = true;
            newEnemy.Tag = "enemy";

            ActiveForm.Controls.Add(newEnemy);
        }
    }
}
