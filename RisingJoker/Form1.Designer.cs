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
            this.GameTimer = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.blueSelectButton = new System.Windows.Forms.Button();
            this.redSelectButton = new System.Windows.Forms.Button();
            this.greenSelectButton = new System.Windows.Forms.Button();
            this.undoColorButton = new System.Windows.Forms.Button();
            this.consoleBoard = new System.Windows.Forms.Label();
            this.startButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // scoreBoard
            // 
            this.scoreBoard.BackColor = System.Drawing.Color.Transparent;
            this.scoreBoard.Font = new System.Drawing.Font("Consolas", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scoreBoard.Location = new System.Drawing.Point(858, 7);
            this.scoreBoard.Name = "scoreBoard";
            this.scoreBoard.Size = new System.Drawing.Size(227, 166);
            this.scoreBoard.TabIndex = 0;
            this.scoreBoard.Tag = "scoreText";
            this.scoreBoard.Text = "Time: 0";
            this.scoreBoard.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.scoreBoard.Visible = false;
            // 
            // GameTimer
            // 
            this.GameTimer.Enabled = true;
            this.GameTimer.Interval = 20;
            this.GameTimer.Tick += new System.EventHandler(this.GameTickEvent);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Consolas", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(11, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(194, 32);
            this.label1.TabIndex = 8;
            this.label1.Tag = "menuText";
            this.label1.Text = "Rising Joker";
            // 
            // blueSelectButton
            // 
            this.blueSelectButton.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.blueSelectButton.ForeColor = System.Drawing.Color.Blue;
            this.blueSelectButton.Location = new System.Drawing.Point(17, 190);
            this.blueSelectButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.blueSelectButton.Name = "blueSelectButton";
            this.blueSelectButton.Size = new System.Drawing.Size(201, 40);
            this.blueSelectButton.TabIndex = 9;
            this.blueSelectButton.TabStop = false;
            this.blueSelectButton.Tag = "menuButton";
            this.blueSelectButton.Text = "Play as Blue";
            this.blueSelectButton.UseVisualStyleBackColor = true;
            this.blueSelectButton.Click += new System.EventHandler(this.OnBlueSelectButtonClick);
            // 
            // redSelectButton
            // 
            this.redSelectButton.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.redSelectButton.ForeColor = System.Drawing.Color.Red;
            this.redSelectButton.Location = new System.Drawing.Point(17, 233);
            this.redSelectButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.redSelectButton.Name = "redSelectButton";
            this.redSelectButton.Size = new System.Drawing.Size(201, 40);
            this.redSelectButton.TabIndex = 10;
            this.redSelectButton.TabStop = false;
            this.redSelectButton.Tag = "menuButton";
            this.redSelectButton.Text = "Play as Red";
            this.redSelectButton.UseVisualStyleBackColor = true;
            this.redSelectButton.Click += new System.EventHandler(this.OnRedSelectButtonClick);
            // 
            // greenSelectButton
            // 
            this.greenSelectButton.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.greenSelectButton.ForeColor = System.Drawing.Color.Green;
            this.greenSelectButton.Location = new System.Drawing.Point(17, 278);
            this.greenSelectButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.greenSelectButton.Name = "greenSelectButton";
            this.greenSelectButton.Size = new System.Drawing.Size(201, 40);
            this.greenSelectButton.TabIndex = 11;
            this.greenSelectButton.TabStop = false;
            this.greenSelectButton.Tag = "menuButton";
            this.greenSelectButton.Text = "Play as Green";
            this.greenSelectButton.UseVisualStyleBackColor = true;
            this.greenSelectButton.Click += new System.EventHandler(this.OnGreenSelectButtonClick);
            // 
            // undoColorButton
            // 
            this.undoColorButton.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.undoColorButton.ForeColor = System.Drawing.Color.Red;
            this.undoColorButton.Location = new System.Drawing.Point(17, 213);
            this.undoColorButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.undoColorButton.Name = "undoColorButton";
            this.undoColorButton.Size = new System.Drawing.Size(201, 80);
            this.undoColorButton.TabIndex = 12;
            this.undoColorButton.TabStop = false;
            this.undoColorButton.Tag = "menuButton";
            this.undoColorButton.Text = "Undo Color";
            this.undoColorButton.UseVisualStyleBackColor = true;
            this.undoColorButton.Visible = false;
            this.undoColorButton.Click += new System.EventHandler(this.OnUndoColorButtonClick);
            // 
            // consoleBoard
            // 
            this.consoleBoard.BackColor = System.Drawing.Color.Transparent;
            this.consoleBoard.Font = new System.Drawing.Font("Consolas", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.consoleBoard.Location = new System.Drawing.Point(693, 173);
            this.consoleBoard.Name = "consoleBoard";
            this.consoleBoard.Size = new System.Drawing.Size(390, 492);
            this.consoleBoard.TabIndex = 13;
            this.consoleBoard.Tag = "menuText";
            this.consoleBoard.Text = "Time: 0";
            this.consoleBoard.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // startButton
            // 
            this.startButton.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startButton.ForeColor = System.Drawing.Color.Black;
            this.startButton.Location = new System.Drawing.Point(17, 350);
            this.startButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(201, 40);
            this.startButton.TabIndex = 14;
            this.startButton.TabStop = false;
            this.startButton.Tag = "menuButton";
            this.startButton.Text = "Start game!";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.OnStartClick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(1095, 674);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.consoleBoard);
            this.Controls.Add(this.greenSelectButton);
            this.Controls.Add(this.redSelectButton);
            this.Controls.Add(this.blueSelectButton);
            this.Controls.Add(this.undoColorButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.scoreBoard);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Tag = "player";
            this.Text = "RisingJoker";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyIsDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyIsUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label scoreBoard;
        private System.Windows.Forms.Timer GameTimer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button blueSelectButton;
        private System.Windows.Forms.Button redSelectButton;
        private System.Windows.Forms.Button greenSelectButton;
        private System.Windows.Forms.Label consoleBoard;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Button undoColorButton;
    }
}

