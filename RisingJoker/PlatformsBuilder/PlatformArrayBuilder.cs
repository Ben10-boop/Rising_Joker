using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RisingJoker.PlatformsBuilder
{
    internal class PlatformArrayBuilder : IPlatformsBuilder
    {
        List<Platform> platforms;
        List<List<MovableObject>> objectsToAdd;
        Dictionary<MoveDirection, int> speeds;
        private Point position;
        private Size size;
        private Color color;

        private int PlatformAmount;
        private int NextPlatformOffset;

        public PlatformArrayBuilder(int platformAmount, int nextPlatformOffset)
        {
            PlatformAmount = platformAmount;
            NextPlatformOffset = nextPlatformOffset;
            Reset();
        }

        public IPlatformsBuilder Reset()
        {
            position = new Point(0, 0);
            size = new Size(250, 20);
            color = Color.Brown;
            objectsToAdd = new List<List<MovableObject>>();
            for (int i = 0; i < PlatformAmount; i++)
            {
                objectsToAdd.Add(new List<MovableObject>());
            }
            speeds = new Dictionary<MoveDirection, int>();
            ResetPlatform();

            return this;
        }
        private void ResetPlatform()
        {
            platforms = null;
        }

        public IPlatformsBuilder SetSize(Size size)
        {
            this.size = size;
            return this;
        }

        public IPlatformsBuilder SetPosition(Point position)
        {
            this.position = position;

            return this;
        }

        public IPlatformsBuilder SetColor(Color color)
        {
            this.color = color;

            return this;
        }
        public IPlatformsBuilder AddCoin(Coin coin, Label form)
        {
            Point moveCoinTo = GetItemPositionOnPlatform(coin, form);
            coin.MoveTo(moveCoinTo);
            this.objectsToAdd[0].Add(coin);
            for (int i = 1; i < PlatformAmount; i++)
            {
                Coin nextCoin = coin.Clone();
                nextCoin.MoveBy(new Point(NextPlatformOffset * i, 0));
                this.objectsToAdd[i].Add(nextCoin);
            }

            return this;
        }
        public IPlatformsBuilder AddEnemy(Enemy enemy, Label form)
        {
            Point moveEnemyTo = GetItemPositionOnPlatform(enemy, form);
            enemy.MoveTo(moveEnemyTo);
            this.objectsToAdd[0].Add(enemy);
            for (int i = 1; i < PlatformAmount; i++)
            {
                Enemy nextEnemy = enemy.Clone();
                nextEnemy.MoveBy(new Point(NextPlatformOffset * i, 0));
                this.objectsToAdd[i].Add(nextEnemy);
            }

            return this;
        }
        private Point GetItemPositionOnPlatform(GameObject other, Label form)
        {
            int xPositionInBounds = Math.Max(Math.Min(position.X + other.position.X, position.X + size.Width - other.size.Width), position.X);

            form.Text = string.Format("OTHER POSITION X: {0} \n position.X: {1} \n Min: {2}", position.X + other.position.X, position.X + size.Width - other.size.Width, Math.Min(position.X + other.position.X, position.X - size.Width));
            return new Point(xPositionInBounds, this.position.Y - other.size.Height);
        }

        public IPlatformsBuilder SetDirectionSpeed(MoveDirection direction, int speed)
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

        public IPlatformsBuilder AddBottom(PlatformBottom bottom, Label form)
        {
            return this;
        }
        public List<Platform> GetPlatform()
        {
            platforms = new List<Platform>();
            for (int i = 0; i < PlatformAmount; i++)
            {
                platforms.Add(new Platform(size, new Point(position.X + NextPlatformOffset * i, position.Y), color));
                SetPlatformSpeeds(platforms[i]);
                objectsToAdd[i].ForEach(obj => platforms[i].AddObject(obj));
            }
            return platforms;
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
    }
}
