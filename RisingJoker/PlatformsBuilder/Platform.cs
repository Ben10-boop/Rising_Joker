using System;
using System.Collections.Generic;
using System.Drawing;

namespace RisingJoker
{
    public class Platform : MovableObject
    {
        public static string TAG = "platform";
        private readonly List<MovableObject> objects;

        public Platform(Size size, Point position, Color color) : base(size, position, true, color, TAG)
        {
            this.objects = new List<MovableObject>();
            //this.objects.Add(new PlatformBottom(new Size(size.Width - 40, 10), new Point(position.X + 20, 15), color));
        }

        public override void OnCollisionWith(GameObject other)
        {
            if (!(other is MovableObject) || other.objectTag != "player")
            {
                return;
            }

            MovableObject obj = (MovableObject)other;

            bool isFalling = obj.GetDirectionSpeed(MoveDirection.Down) + obj.GetDirectionSpeed(MoveDirection.Up) > 0;
            Rectangle objBounds = obj.GetBounds();
            Rectangle platformBounds = this.GetBounds();
            if (!isFalling)
            {
                this.ChangeColor(Color.BlueViolet);
                return;
            }
            int threshold = 15;
            bool comingFromTop = objBounds.Bottom >= (platformBounds.Top - 4) && objBounds.Top < platformBounds.Top && objBounds.Bottom - (platformBounds.Top - 4) <= threshold;
            if (comingFromTop)
            {
                this.ChangeColor(Color.Red);
                obj.MoveBy(new Point(0, platformBounds.Top - objBounds.Bottom - 7));
            }

        }

        public override void Move()
        {
            Point moveChildrenBy = new Point(0, this.DownDirectionSpeed);
            objects.ForEach(o =>
            {
                o.MoveBy(moveChildrenBy);
            });

            base.Move();
        }

        public override void MoveDisplayObject()
        {
            objects.ForEach(o =>
            {
                o.MoveDisplayObject();
            });

            base.MoveDisplayObject();
        }

        public void AddObject(MovableObject item)
        {
            objects.Add(item);
        }

        public void RemoveObject(MovableObject item)
        {
            if (objects.Find(o => o == item) == null)
            {
                return;
            }

            objects.Remove(item);
        }

        private void RemoveObject(object sender, EventArgs e)
        {
            RemoveObject(sender as MovableObject);
        }

        public override void Render()
        {
            objects.ForEach(o =>
            {
                o.Render();
                o.ObjectDestruction += RemoveObject;
            });
            base.Render();
        }
    }
}
