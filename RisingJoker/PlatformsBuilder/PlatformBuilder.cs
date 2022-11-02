using RisingJoker.BaseGameObjects;
using RisingJoker.PlatformsBuilder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace RisingJoker
{
    public class PlatformBuilder : IPlatformBuilder
    {
        Platform platform;
        List<IMovableObject> objectsToAdd;
        Dictionary<MoveDirection, int> speeds;
        private Point position;
        private Size size;
        private Color color;

        public PlatformBuilder()
        {
            Reset();
        }

        public IPlatformBuilder Reset()
        {
            position = new Point(0, 0);
            size = new Size(250, 20);
            color = Color.Brown;
            objectsToAdd = new List<IMovableObject>();
            speeds = new Dictionary<MoveDirection, int>();
            ResetPlatform();

            return this;
        }

        private void ResetPlatform()
        {
            platform = null;
        }

        public IPlatformBuilder SetSize(Size size)
        {
            this.size = size;
            return this;
        }

        public IPlatformBuilder SetPosition(Point position)
        {
            this.position = position;

            return this;
        }

        public IPlatformBuilder SetColor(Color color)
        {
            this.color = color;

            return this;
        }

        public IPlatformBuilder AddObjToPlatform(IMovableObject obj, Label form, bool below = false)
        {
            Point moveObjTo = GetItemPositionOnPlatform(obj, form, below);
            obj.MoveTo(moveObjTo);
            objectsToAdd.Add(obj);

            return this;
        }


        private void RestrictObject(IMovableObject obj)
        {
            obj.ParentXStart = position.X;
            obj.ParentXEnd = position.X + size.Width;
        }

        private Point GetItemPositionOnPlatform(IMovableObject other, Label form, bool below)
        {
            int xPositionInBounds = Math.Max(Math.Min(position.X + other.position.X, position.X + size.Width - other.size.Width), position.X);

            form.Text = string.Format("OTHER POSITION X: {0} \n position.X: {1} \n Min: {2}", position.X + other.position.X, position.X + size.Width - other.size.Width, Math.Min(position.X + other.position.X, position.X - size.Width));
            return new Point(xPositionInBounds, position.Y - (below ? -size.Height : other.size.Height));
        }

        public Platform GetPlatform()
        {
            if (this.platform != null)
            {
                return this.platform;
            }

            platform = new Platform(size, position, color);
            SetPlatformSpeeds(platform);
            SetupPlatformObjects(platform);

            return platform;
        }

        private void SetupPlatformObjects(Platform platform)
        {
            objectsToAdd.ForEach(obj =>
            {
                RestrictObject(obj);
                platform.AddObject(obj);
            });
        }

        private void SetPlatformSpeeds(Platform platform)
        {
            if (speeds.ContainsKey(MoveDirection.Up))
            {
                platform.UpDirectionSpeed = speeds[MoveDirection.Up];
            }
            if (speeds.ContainsKey(MoveDirection.Down))
            {
                platform.DownDirectionSpeed = speeds[MoveDirection.Down];
            }
            if (speeds.ContainsKey(MoveDirection.Left))
            {
                platform.LeftDirectionSpeed = speeds[MoveDirection.Left];
            }
            if (speeds.ContainsKey(MoveDirection.Right))
            {
                platform.RightDirectionSpeed = speeds[MoveDirection.Right];
            }
        }

        public IPlatformBuilder SetDirectionSpeed(MoveDirection direction, int speed)
        {
            switch (direction)
            {
                case MoveDirection.Up:
                    speeds[MoveDirection.Up] = speed;
                    break;
                case MoveDirection.Down:
                    speeds[MoveDirection.Down] = speed;
                    break;
                case MoveDirection.Left:
                    speeds[MoveDirection.Left] = speed;
                    break;
                case MoveDirection.Right:
                    speeds[MoveDirection.Right] = speed;
                    break;
            }

            return this;
        }
    }
}
