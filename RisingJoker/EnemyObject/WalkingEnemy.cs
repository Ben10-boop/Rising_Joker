using RisingJoker.BaseGameObjects;
using RisingJoker.PlatformFactory;
using System;
using System.Drawing;

namespace RisingJoker.EnemyObject
{
    public class WalkingEnemy : EnemyMovementDecorator
    {
        private MoveDirection direction = MoveDirection.Left;
        public WalkingEnemy(IEnemy baseEnemy) : base(baseEnemy)
        { }

        public override void Move()
        {
            BaseEnemy.Move();
            if (ShouldChangeDirection())
            {
                ChangeDirection();
            }

            BaseEnemy.position = new Point(GetNewXCoordinate(), BaseEnemy.position.Y);

        }

        private int GetNewXCoordinate()
        {
            switch (direction)
            {
                case MoveDirection.Left:
                    return Math.Max(BaseEnemy.position.X - 2, BaseEnemy.ParentXStart);
                default:
                    return Math.Min(BaseEnemy.position.X + 2, BaseEnemy.ParentXEnd - BaseEnemy.info.size.Width);
            };
        }

        private bool ShouldChangeDirection()
        {
            return (direction == MoveDirection.Left && BaseEnemy.position.X <= BaseEnemy.ParentXStart)
                || (direction == MoveDirection.Right && BaseEnemy.position.X >= BaseEnemy.ParentXEnd - BaseEnemy.info.size.Width);
        }

        private void ChangeDirection()
        {
            switch (direction)
            {
                case MoveDirection.Left:
                    direction = MoveDirection.Right;
                    break;
                default:
                    direction = MoveDirection.Left;
                    break;
            }
        }

        public override IEnemy Clone()
        {
            return new WalkingEnemy(BaseEnemy.Clone());
        }
    }
}
