using System.Collections.Generic;
using System.Drawing;

namespace RisingJoker
{
    public class PlatformBuilder
    {
        Platform platform;
        List<MovableObject> objectsToAdd;
        Dictionary<MoveDirection, int> speeds;
        private Point position;
        private Size size;
        private Color color;

        public PlatformBuilder()
        {
            Reset();
        }

        public PlatformBuilder Reset()
        {
            position = new Point(0, 0);
            size = new Size(250, 20);
            color = Color.Brown;
            objectsToAdd = new List<MovableObject>();
            speeds = new Dictionary<MoveDirection, int>();
            ResetPlatform();

            return this;
        }

        private void ResetPlatform()
        {
            platform = null;
        }

        public PlatformBuilder SetSize(Size size)
        {
            this.size = size;
            ResetPlatform();
            return this;
        }

        public PlatformBuilder SetPosition(Point position)
        {
            this.position = position;
            ResetPlatform();

            return this;
        }

        public PlatformBuilder SetColor(Color color)
        {
            this.color = color;
            ResetPlatform();

            return this;
        }



        public PlatformBuilder AddCoin(int itemPositionX)
        {
            Point coinPosition = GetItemPositionOnPlatform(itemPositionX);
            Coin coin = CoinFactory.CreateCoin(coinPosition);
            this.objectsToAdd.Add(coin);
            ResetPlatform();

            return this;
        }

        private Point GetItemPositionOnPlatform(int itemPositionX)
        {
            return new Point(position.X + itemPositionX, position.Y);
        }

        public Platform GetPlatform()
        {
            if (this.platform != null)
            {
                return this.platform;
            }
            this.platform = new Platform(size, position, color);
            SetPlatformSpeeds(platform);
            SetPlatformObjects(platform);

            return platform;
        }

        private void SetPlatformObjects(Platform platform)
        {
            objectsToAdd.ForEach(obj => platform.AddObject(obj));
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

        public PlatformBuilder SetDirectionSpeed(MoveDirection direction, int speed)
        {
            ResetPlatform();
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

        public PlatformBuilder AddEnemy(int itemPositionX)
        {
            ResetPlatform();
            Point enemyPosition = GetItemPositionOnPlatform(itemPositionX);
            Enemy enemy = EnemyFactory.CreateEnemy(enemyPosition);
            this.objectsToAdd.Add(enemy);
            return this;
        }
    }
}
