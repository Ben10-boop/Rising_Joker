using RisingJoker.BaseGameObjects;
using RisingJoker.PlatformFactory;
using System.Drawing;

namespace RisingJoker.EnemyObject
{
    public class HoveringEnemy : EnemyMovementDecorator
    {
        private const int MaxMove = 25;
        private const int MinMove = 5;
        private const int HoverBy = 1;
        private MoveDirection direction = MoveDirection.Up;
        private int currentY = 0;
        public HoveringEnemy(IEnemy baseEnemy) : base(baseEnemy)
        { }

        public override void Move()
        {
            BaseEnemy.Move();
            if (ShouldChangeDirection())
            {
                ChangeDirection();
            }
            BaseEnemy.position = new Point(BaseEnemy.position.X, GetNewY());
        }

        private int GetNewY()
        {
            switch (direction)
            {
                case MoveDirection.Up:
                    currentY += HoverBy;
                    return BaseEnemy.position.Y - HoverBy;
                default:
                    currentY -= HoverBy;
                    return BaseEnemy.position.Y + HoverBy;

            }

        }

        private bool ShouldChangeDirection()
        {
            return (direction == MoveDirection.Up && currentY == MaxMove)
                || (direction == MoveDirection.Down && currentY == MinMove);
        }

        private void ChangeDirection()
        {
            switch (direction)
            {
                case MoveDirection.Up:
                    direction = MoveDirection.Down;
                    break;
                default:
                    direction = MoveDirection.Up;
                    break;
            }
        }

        public override IEnemy Clone()
        {
            return new HoveringEnemy(BaseEnemy.Clone());
        }
    }
}
