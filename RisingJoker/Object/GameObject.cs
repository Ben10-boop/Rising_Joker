using System;
using System.Drawing;
using System.Windows.Forms;

namespace RisingJoker.Object
{
    public abstract class GameObject : IGameObject
    {
        protected PictureBox objectDisplay;
        public virtual event EventHandler ObjectDestruction;
        public static Form gameScreen;
        public virtual Size size { get; set; }
        public virtual Point position { get; set; }
        public virtual bool isVisible { get; set; }
        public virtual Color color { get; set; }
        public string objectTag { get; set; }

        public GameObject(Size size, Point position, bool isVisible, Color color, string objectTag)
        {
            this.size = size;
            this.position = position;
            this.isVisible = isVisible;
            this.color = color;
            this.objectTag = objectTag;
        }

        public virtual void OnCollisionWith(IGameObject other)
        { }

        public virtual bool IsCollidingWith(IGameObject other)
        {
            return this.GetBounds().IntersectsWith(other.GetBounds());
        }


        public virtual Rectangle GetBounds()
        {
            return this.objectDisplay.Bounds;
        }

        public virtual void ChangeColor(Color color)
        {
            this.objectDisplay.BackColor = color;
        }

        public virtual void CreateObject()
        {
            objectDisplay = new PictureBox();
            objectDisplay.BackColor = color;
            objectDisplay.Size = size;
            objectDisplay.Location = position;
            objectDisplay.Visible = isVisible;
            objectDisplay.Tag = objectTag;
            GetGameScreen().Controls.Add(objectDisplay);
        }

        public virtual void Render()
        {
            if (objectDisplay == null)
                return;
            if (!objectDisplay.BackColor.Equals(color))
                objectDisplay.BackColor = color;

            if (!objectDisplay.Size.Equals(size))
                objectDisplay.Size = size;

            if (!objectDisplay.Location.Equals(position))
            {
                objectDisplay.Location = position;
            }

            if (objectDisplay.Visible != isVisible)
                objectDisplay.Visible = isVisible;
        }

        protected virtual void OnDestruction(EventArgs e)
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
