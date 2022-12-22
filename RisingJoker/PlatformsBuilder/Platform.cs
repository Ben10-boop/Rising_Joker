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
            if (mediator != null)
            {
                mediator.React(this, other);
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

        public override bool IsObjectAlive()
        {
            return objects.Any(o => o.IsObjectAlive()) || base.IsObjectAlive();
        }
    }
}
