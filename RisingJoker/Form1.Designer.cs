namespace RisingJoker
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.scoreBoard = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.player = new System.Windows.Forms.PictureBox();
            this.GameTimer = new System.Windows.Forms.Timer(this.components);
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.blueSelectButton = new System.Windows.Forms.Button();
            this.redSelectButton = new System.Windows.Forms.Button();
            this.greenSelectButton = new System.Windows.Forms.Button();
            this.consoleBoard = new System.Windows.Forms.Label();
            this.startButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.player)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            this.SuspendLayout();
            // 
            // scoreBoard
            // 
            this.scoreBoard.Font = new System.Drawing.Font("Consolas", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scoreBoard.Location = new System.Drawing.Point(466, 9);
            this.scoreBoard.Name = "scoreBoard";
            this.scoreBoard.Size = new System.Drawing.Size(255, 81);
            this.scoreBoard.TabIndex = 0;
            this.scoreBoard.Tag = "scoreText";
            this.scoreBoard.Text = "Time: 0";
            this.scoreBoard.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.scoreBoard.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Brown;
            this.pictureBox1.Location = new System.Drawing.Point(12, 437);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(709, 32);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Tag = "platform";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Brown;
            this.pictureBox2.Location = new System.Drawing.Point(402, 309);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(236, 32);
            this.pictureBox2.TabIndex = 2;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Tag = "platform";
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.Brown;
            this.pictureBox3.Location = new System.Drawing.Point(128, 186);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(236, 32);
            this.pictureBox3.TabIndex = 3;
            this.pictureBox3.TabStop = false;
            this.pictureBox3.Tag = "platform";
            // 
            // player
            // 
            this.player.BackColor = System.Drawing.Color.Blue;
            this.player.Location = new System.Drawing.Point(89, 386);
            this.player.Name = "player";
            this.player.Size = new System.Drawing.Size(45, 45);
            this.player.TabIndex = 4;
            this.player.TabStop = false;
            this.player.Tag = "player";
            this.player.Visible = false;
            // 
            // GameTimer
            // 
            this.GameTimer.Enabled = true;
            this.GameTimer.Interval = 20;
            this.GameTimer.Tick += new System.EventHandler(this.GameTickEvent);
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackColor = System.Drawing.Color.Brown;
            this.pictureBox4.Location = new System.Drawing.Point(402, 118);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(236, 32);
            this.pictureBox4.TabIndex = 5;
            this.pictureBox4.TabStop = false;
            this.pictureBox4.Tag = "platform";
            // 
            // pictureBox5
            // 
            this.pictureBox5.BackColor = System.Drawing.Color.Brown;
            this.pictureBox5.Location = new System.Drawing.Point(128, -3);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(236, 32);
            this.pictureBox5.TabIndex = 6;
            this.pictureBox5.TabStop = false;
            this.pictureBox5.Tag = "platform";
            // 
            // pictureBox6
            // 
            this.pictureBox6.BackColor = System.Drawing.Color.Brown;
            this.pictureBox6.Location = new System.Drawing.Point(150, 205);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(187, 13);
            this.pictureBox6.TabIndex = 7;
            this.pictureBox6.TabStop = false;
            this.pictureBox6.Tag = "platform";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Consolas", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 113);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(233, 37);
            this.label1.TabIndex = 8;
            this.label1.Tag = "menuText";
            this.label1.Text = "Rising Joker";
            // 
            // blueSelectButton
            // 
            this.blueSelectButton.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.blueSelectButton.ForeColor = System.Drawing.Color.Blue;
            this.blueSelectButton.Location = new System.Drawing.Point(19, 237);
            this.blueSelectButton.Name = "blueSelectButton";
            this.blueSelectButton.Size = new System.Drawing.Size(226, 50);
            this.blueSelectButton.TabIndex = 9;
            this.blueSelectButton.TabStop = false;
            this.blueSelectButton.Tag = "menuButton";
            this.blueSelectButton.Text = "Play as Blue";
            this.blueSelectButton.UseVisualStyleBackColor = true;
            this.blueSelectButton.Click += new System.EventHandler(this.blueSelectButton_Click);
            // 
            // redSelectButton
            // 
            this.redSelectButton.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.redSelectButton.ForeColor = System.Drawing.Color.Red;
            this.redSelectButton.Location = new System.Drawing.Point(19, 291);
            this.redSelectButton.Name = "redSelectButton";
            this.redSelectButton.Size = new System.Drawing.Size(226, 50);
            this.redSelectButton.TabIndex = 10;
            this.redSelectButton.TabStop = false;
            this.redSelectButton.Tag = "menuButton";
            this.redSelectButton.Text = "Play as Red";
            this.redSelectButton.UseVisualStyleBackColor = true;
            this.redSelectButton.Click += new System.EventHandler(this.redSelectButton_Click);
            // 
            // greenSelectButton
            // 
            this.greenSelectButton.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.greenSelectButton.ForeColor = System.Drawing.Color.Green;
            this.greenSelectButton.Location = new System.Drawing.Point(19, 347);
            this.greenSelectButton.Name = "greenSelectButton";
            this.greenSelectButton.Size = new System.Drawing.Size(226, 50);
            this.greenSelectButton.TabIndex = 11;
            this.greenSelectButton.TabStop = false;
            this.greenSelectButton.Tag = "menuButton";
            this.greenSelectButton.Text = "Play as Green";
            this.greenSelectButton.UseVisualStyleBackColor = true;
            this.greenSelectButton.Click += new System.EventHandler(this.greenSelectButton_Click);
            // 
            // consoleBoard
            // 
            this.consoleBoard.Font = new System.Drawing.Font("Consolas", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.consoleBoard.Location = new System.Drawing.Point(283, 99);
            this.consoleBoard.Name = "consoleBoard";
            this.consoleBoard.Size = new System.Drawing.Size(438, 452);
            this.consoleBoard.TabIndex = 12;
            this.consoleBoard.Tag = "menuText";
            this.consoleBoard.Text = "Time: 0";
            this.consoleBoard.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // startButton
            // 
            this.startButton.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startButton.ForeColor = System.Drawing.Color.Black;
            this.startButton.Location = new System.Drawing.Point(19, 437);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(226, 50);
            this.startButton.TabIndex = 13;
            this.startButton.TabStop = false;
            this.startButton.Tag = "menuButton";
            this.startButton.Text = "Start game!";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(733, 613);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.consoleBoard);
            this.Controls.Add(this.greenSelectButton);
            this.Controls.Add(this.redSelectButton);
            this.Controls.Add(this.blueSelectButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox6);
            this.Controls.Add(this.pictureBox5);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.player);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.scoreBoard);
            this.Name = "Form1";
            this.Tag = "player";
            this.Text = "RisingJoker";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyIsDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyIsUp);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.player)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label scoreBoard;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox player;
        private System.Windows.Forms.Timer GameTimer;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.PictureBox pictureBox6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button blueSelectButton;
        private System.Windows.Forms.Button redSelectButton;
        private System.Windows.Forms.Button greenSelectButton;
        private System.Windows.Forms.Label consoleBoard;
        private System.Windows.Forms.Button startButton;
    }
}

