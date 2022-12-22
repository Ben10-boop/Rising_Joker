using RisingJoker.BaseGameObjects;
using System;
using System.Drawing;

namespace RisingJoker.StatefulMascot
{
    public class FastTrollingMascotState : MascotState
    {
        private int speed = 10;
        private int focusedCorner = 0;

        public FastTrollingMascotState(Player player, Mascot mascot) : base(player, mascot)
        {
            mascot.ChangeColor(Color.Black);

        }

        public override void Move()
        {
            var height = GameObject.gameScreen.Size.Height - 65;
            var width = GameObject.gameScreen.Size.Width - 40;

            var position = mascot.position;
            int newX = position.X;
            int newY = position.Y;

            if (focusedCorner == 0)
            {
                var cornerX = 0;
                var cornerY = height;

                if (cornerX >= position.X && cornerY <= position.Y)
                {
                    focusedCorner++;
                    return;

                }
                newX = cornerX >= position.X ? cornerX : position.X - speed;
                newY = cornerY <= position.Y ? cornerY : position.Y + speed;

            }
            else if (focusedCorner == 1)
            {
                var cornerX = width;
                var cornerY = height;

                if (cornerX <= position.X && cornerY <= position.Y)
                {
                    focusedCorner++;
                    return;
                }

                newX = cornerX <= position.X ? cornerX : position.X + speed;
                newY = cornerY <= position.Y ? cornerY : position.Y + speed;
            }
            else if (focusedCorner == 2)
            {
                var cornerX = width;
                var cornerY = 0;

                if (cornerX <= position.X && cornerY >= position.Y)
                {
                    focusedCorner++;
                    return;
                }

                newX = cornerX <= position.X ? cornerX : position.X + speed;
                newY = cornerY >= position.Y ? cornerY : position.Y - speed;
            }
            else
            {
                var cornerX = 0;
                var cornerY = 0;

                if (cornerX >= position.X && cornerY >= position.Y)
                {
                    focusedCorner = 0;
                    return;

                }

                newX = cornerX >= position.X ? cornerX : position.X - speed;
                newY = cornerY >= position.Y ? cornerY : position.Y - speed;
            }

            mascot.MoveTo(new Point(newX, newY));
        }

        public override void OnCollisionWith(IGameObject other)
        {

        }

        public override void RandomizeNextState()
        {
            Random rand = new Random();
            var value = rand.Next(0, 4);

            if (value > 2)
            {
                mascot.ChangeState(new FollowingMascotState(player, mascot));
            }
            else
            {
                mascot.ChangeState(new IdleMascotState(player, mascot));
            }
        }
    }
}
