using RisingJoker.BaseGameObjects;
using System;
using System.Drawing;

namespace RisingJoker.StatefulMascot
{
    public class FollowingMascotState : MascotState
    {
        private int speed = 5;
        private int focusedCorner = 0;

        public FollowingMascotState(Player player, Mascot mascot) : base(player, mascot)
        {
            mascot.ChangeColor(Color.MistyRose);

        }

        public override void Move()
        {
            var height = GameObject.gameScreen.Size.Height - 65;
            var width = GameObject.gameScreen.Size.Width - 40;

            var position = mascot.position;

            var newY = height <= position.Y ? height : position.Y + speed;
            var newX = position.X == player.position.X ? position.X : position.X < player.position.X ? position.X + speed : position.X - speed;
            if (Math.Abs(newX - player.position.X) <= speed)
            {
                newX = player.position.X;
            }

            mascot.MoveTo(new Point(newX, newY));
        }

        public override void OnCollisionWith(IGameObject other)
        {

        }

        public override void RandomizeNextState()
        {
            Random rand = new Random();
            var value = rand.Next(0, 6);

            if (value > 3)
            {
                mascot.ChangeState(new TrollingMascotState(player, mascot));
            }
            else
            {
                mascot.ChangeState(new TrollingMascotState(player, mascot));
            }
        }
    }
}
