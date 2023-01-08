using System;
using System.Drawing;
using System.Windows.Forms;

namespace RisingJoker
{
    public enum MoveDirection
    {
        Left, Right, Up, Down
    }

    public abstract class GameObject
    {
        protected PictureBox objectDisplay;
        public event EventHandler ObjectDestruction;
        public static Form gameScreen;
        public Size size;
        public Point position;
        protected bool isVisible;
        protected Color color;
        public string objectTag;
        public virtual int LeftDirectionSpeed { get; set; }
        public virtual int RightDirectionSpeed { get; set; }
        public virtual int UpDirectionSpeed { get; set; }
        public virtual int DownDirectionSpeed { get; set; }


        public GameObject(Size size, Point position, bool isVisible, Color color, string objectTag)
        {
            this.size = size;
            this.position = position;
            this.isVisible = isVisible;
            this.color = color;
            this.objectTag = objectTag;
        }

        public abstract void OnCollisionWith(GameObject other);

        public virtual bool IsCollidingWith(GameObject other)
        {

            return this.GetBounds().IntersectsWith(other.GetBounds());
        }


        public Rectangle GetBounds()
        {
            return this.objectDisplay.Bounds;
        }

        public void ChangeColor(Color color)
        {
            this.objectDisplay.BackColor = color;
        }

        public virtual void Render()
        {
            this.objectDisplay = new PictureBox();
            objectDisplay.BackColor = this.color;
            objectDisplay.Size = this.size;
            objectDisplay.Location = this.position;
            objectDisplay.Visible = this.isVisible;
            objectDisplay.Tag = this.objectTag;
            this.GetGameScreen().Controls.Add(objectDisplay);
        }

        protected void OnDestruction(EventArgs e)
        {
            ObjectDestruction?.Invoke(this, e);
        }

        protected Form GetGameScreen()
        {
            if (GameObject.gameScreen == null)
                throw new Exception("Game Screen not set!");

            return GameObject.gameScreen;
        }

        public static void SetGameScreen(Form gameScreen)
        {
            GameObject.gameScreen = gameScreen;
        }

        public virtual void Move()
        {
            this.position = new Point(position.X + this.LeftDirectionSpeed + this.RightDirectionSpeed, position.Y + this.UpDirectionSpeed + this.DownDirectionSpeed);
        }

        public virtual void MoveBy(Point moveBy)
        {
            this.position = new Point(position.X + moveBy.X, position.Y + moveBy.Y);
        }

        public virtual void MoveTo(Point moveTo)
        {
            this.position = new Point(moveTo.X, moveTo.Y);

        }

        public virtual void MoveObjectInDisplay()
        {
            this.objectDisplay.Left = position.X;
            this.objectDisplay.Top = position.Y;
        }

        public virtual int GetDirectionSpeed(MoveDirection direction)
        {
            switch (direction)
            {
                case MoveDirection.Up:
                    return UpDirectionSpeed;
                case MoveDirection.Down:
                    return DownDirectionSpeed;
                case MoveDirection.Right:
                    return RightDirectionSpeed;
                case MoveDirection.Left:
                    return LeftDirectionSpeed;
                default:
                    return 0;
            }
        }
    }
}
