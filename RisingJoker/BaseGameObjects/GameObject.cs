using System;
using System.Drawing;
using System.Windows.Forms;

namespace RisingJoker.BaseGameObjects
{
    public abstract class GameObject : IGameObject
    {
        protected virtual PictureBox objectDisplay { get; set; }
        public virtual event EventHandler ObjectDestruction;
        public static Form gameScreen;
        public virtual Size size { get; set; }
        public virtual Point position { get; set; }
        protected virtual bool isVisible { get; set; }
        public virtual Color color { get; set; }
        public virtual string objectTag { get; set; }
        public virtual bool isAlive { get; set; }

        public GameObject(Size size, Point position, bool isVisible, Color color, string objectTag)
        {
            this.size = size;
            this.position = position;
            this.isVisible = isVisible;
            this.color = color;
            this.objectTag = objectTag;
            isAlive = true;
        }

        public virtual void OnCollisionWith(IGameObject other)
        { }

        public virtual bool IsCollidingWith(IGameObject other)
        {

            return this.GetBounds().IntersectsWith(other.GetBounds());
        }


        public virtual Rectangle GetBounds()
        {
            if (objectDisplay == null)
                CreateObject();

            return this.objectDisplay.Bounds;
        }

        public virtual void ChangeColor(Color color)
        {
            this.objectDisplay.BackColor = color;
        }

        private void CreateObject()
        {
            this.objectDisplay = new PictureBox();
            this.GetGameScreen().Controls.Add(objectDisplay);
        }

        public virtual void Render()
        {
            if (objectDisplay == null)
                CreateObject();

            objectDisplay.BackColor = color;
            objectDisplay.Size = size;
            objectDisplay.Location = position;
            objectDisplay.Visible = isVisible;
            objectDisplay.Tag = objectTag;
        }

        protected virtual void OnDestruction(EventArgs e)
        {
            ObjectDestruction?.Invoke(this, e);
        }

        protected virtual Form GetGameScreen()
        {
            if (GameObject.gameScreen == null)
                throw new Exception("Game Screen not set!");

            return GameObject.gameScreen;
        }

        public static void SetGameScreen(Form gameScreen)
        {
            GameObject.gameScreen = gameScreen;
        }

        public virtual bool IsObjectAlive()
        {
            return GetGameScreen().Bottom - GetGameScreen().Top > position.Y - size.Height && isAlive;
        }

        public virtual void RemoveFromScreen()
        {
            if (objectDisplay != null)
            {
                this.GetGameScreen().Controls.Remove(objectDisplay);
                objectDisplay.Dispose();
                isAlive = false;
            }
        }
    }
}
