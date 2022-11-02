using RisingJoker.BaseGameObjects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace RisingJoker
{
    public class Platform : MovableObject
    {
        public static string TAG = "platform";
        private readonly List<IMovableObject> objects;

        public Platform(Size size, Point position, Color color) : base(size, position, true, color, TAG)
        {
            this.objects = new List<IMovableObject>();
        }

        public override void OnCollisionWith(IGameObject other)
        {
            if (!(other is Player) || other.objectTag != "player")
            {
                return;
            }

            Player obj = (Player)other;

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

        public void AddObject(IMovableObject item)
        {
            objects.Add(item);
            item.ObjectDestruction += RemoveObject;
        }

        public void RemoveObject(IMovableObject item)
        {
            if (objects.Find(o => o == item) == null)
            {
                return;
            }

            objects.Remove(item);
        }

        private void RemoveObject(object sender, EventArgs e)
        {
            RemoveObject(sender as IMovableObject);
        }

        public override bool IsInScreen()
        {
            return objects.Any(o => o.IsInScreen()) || base.IsInScreen();
        }
    }
}
