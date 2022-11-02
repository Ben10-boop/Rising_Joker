using RisingJoker.PlatformFactory;
using System;
using System.Drawing;

namespace RisingJoker.EnemyObject
{
    public class TeleportingEnemy : EnemyMovementDecorator
    {
        private double Cooldown = 0.0;
        private Random PositionRandomizer;
        public TeleportingEnemy(IEnemy baseEnemy) : base(baseEnemy)
        {
            Cooldown = 0.0;
            PositionRandomizer = new Random();
        }

        public override void Move()
        {
            BaseEnemy.Move();
            if (Cooldown > 0)
            {
                Cooldown -= 0.2;
                return;
            }
            if (Cooldown < 0)
            {
                Cooldown = 0.0;
            }

            BaseEnemy.position = new Point(GenerateNewXPosition(), BaseEnemy.position.Y);

            Cooldown = 15.0;
        }

        private int GenerateNewXPosition()
        {
            return PositionRandomizer.Next(BaseEnemy.ParentXStart, BaseEnemy.ParentXEnd - BaseEnemy.size.Width);
        }

        public override IEnemy Clone()
        {
            return new TeleportingEnemy(BaseEnemy.Clone());
        }
    }
}
