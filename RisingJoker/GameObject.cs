using System;
using System.Drawing;
using System.Windows.Forms;

namespace RisingJoker
{
    public abstract class GameObject // : IClonable
    {
        protected PictureBox objectDisplay;
        public event EventHandler ObjectDestruction;
        public static Form gameScreen;
        public Size size;
        public Point position;
        protected bool isVisible;
        protected Color color;
        public string objectTag;

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
    }
}
