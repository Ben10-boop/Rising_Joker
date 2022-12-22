using RisingJoker.BaseGameObjects;
using System;
using System.Drawing;

namespace RisingJoker.StatefulMascot
{
    public class IdleMascotState : MascotState
    {
        private int speed = 5;
        private int focusedCorner = 0;

        public IdleMascotState(Player player, Mascot mascot) : base(player, mascot)
        {
            mascot.ChangeColor(Color.Brown);

        }

        public override void Move()
        {

        }

        public override void OnCollisionWith(IGameObject other)
        {
            if (other is Player parentPlayer && player == parentPlayer)
            {
                bool isFalling = player.GetDirectionSpeed(MoveDirection.Down) + player.GetDirectionSpeed(MoveDirection.Up) > 0;
                if (!isFalling || player.isJumping && player.hasLanded && player.jumpCooldown == 0)
                {
                    return;
                }
                Rectangle objBounds = player.GetBounds();
                Rectangle platformBounds = mascot.GetBounds();
                int threshold = 15;
                bool comingFromTop = objBounds.Bottom >= (platformBounds.Top - 4) && objBounds.Top < platformBounds.Top && objBounds.Bottom - (platformBounds.Top - 4) <= threshold;
                if (comingFromTop)
                {
                    player.MoveBy(new Point(0, platformBounds.Top - objBounds.Bottom - 7));
                }
            }
        }

        public override void RandomizeNextState()
        {
            Random rand = new Random();
            var value = rand.Next(0, 10);

            if (value > 4)
            {
                mascot.ChangeState(new FollowingMascotState(player, mascot));
            }
            else if (value > 7)
            {
                mascot.ChangeState(new FastTrollingMascotState(player, mascot));

            }
            else
            {
                mascot.ChangeState(new TrollingMascotState(player, mascot));
            }
        }
    }
}
