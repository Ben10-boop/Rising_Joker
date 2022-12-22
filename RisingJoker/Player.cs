using RisingJoker.BaseGameObjects;
using RisingJoker.PointsObserver;
using System.Collections.Generic;
using System.Drawing;

namespace RisingJoker
{
    public abstract class Player : MovableObject, IPointsDispatcher
    {
        public static string TAG = "player";
        const int HORIZONTAL_SPEED = 7;
        const int FALL_DOWN_SPEED = 10;
        const int JUMP_SPEED = -FALL_DOWN_SPEED - 20;
        private double jumpCooldown = 0;
        private bool isJumping, isMovingLeft, isMovingRight = false;
        private bool hasLanded = true;
        public int Points { get; set; }
        protected List<IPointsListener> Listeners = new List<IPointsListener>();

        public Player(Size size, Point position, bool isVisible, Color color, int points) : base(size, position, isVisible, color, TAG)
        {
            LeftDirectionSpeed = -HORIZONTAL_SPEED;
            RightDirectionSpeed = HORIZONTAL_SPEED;
            UpDirectionSpeed = 0;
            DownDirectionSpeed = FALL_DOWN_SPEED;
            Points = points;
        }

        public void UpdateUniqueMechanicPoints(double currentGameTime)
        {
            if (isAlive)
                Notify(currentGameTime);
        }

        protected virtual void Notify(double currentGameTime)
        {

        }

        public void Subscribe(IPointsListener listener)
        {
            if (Listeners.Contains(listener))
                return;
            Listeners.Add(listener);
        }

        public void Unsubscribe(IPointsListener listener)
        {
            if (!Listeners.Contains(listener))
                return;
            Listeners.Remove(listener);
        }

        public override void OnCollisionWith(IGameObject other)
        {
            if (mediator != null)
            {
                mediator.React(this, other);
            }
        }

        public void landPlayer()
        {
            if (jumpCooldown <= 0)
            {
                hasLanded = true;
            }
        }

        public override void Move()
        {
            if (isMovingLeft)
            {
                HorizontalMove(MoveDirection.Left);
            }
            if (isMovingRight)
            {
                HorizontalMove(MoveDirection.Right);
            }
            VerticalMove();
        }
        public void SetMovement(MoveDirection direction, bool isMoving)
        {
            switch (direction)
            {
                case MoveDirection.Left:
                    isMovingLeft = isMoving;
                    break;
                case MoveDirection.Right:
                    isMovingRight = isMoving;
                    break;
                case MoveDirection.Up:
                    isJumping = isMoving;
                    break;
            }
        }

        private int GetHorizontalSpeed(MoveDirection direction)
        {
            switch (direction)
            {
                case MoveDirection.Left:
                    return LeftDirectionSpeed;
                case MoveDirection.Right:
                    return RightDirectionSpeed;
                default:
                    return 0;
            }
        }

        private int GetVerticalSpeed(MoveDirection direction)
        {
            switch (direction)
            {
                case MoveDirection.Up:
                    return DownDirectionSpeed + UpDirectionSpeed;
                default:
                    return DownDirectionSpeed;
            }
        }

        public override int GetDirectionSpeed(MoveDirection direction)
        {
            switch (direction)
            {
                case MoveDirection.Up:
                case MoveDirection.Down:
                    return GetVerticalSpeed(direction);
                case MoveDirection.Left:
                case MoveDirection.Right:
                    return GetHorizontalSpeed(direction);
                default:
                    return 0;
            }
        }

        private void HorizontalMove(MoveDirection direction)
        {
            position = new Point(position.X + GetHorizontalSpeed(direction), position.Y);
        }

        private void VerticalMove()
        {
            if (jumpCooldown > 0)
            {
                jumpCooldown -= 2.0 / 20;
                ChangeColor(Color.IndianRed);
            }
            if (jumpCooldown < 0)
            {
                jumpCooldown = 0;
            }
            if (UpDirectionSpeed < 0)
            {
                UpDirectionSpeed -= JUMP_SPEED / 20;
            }
            if (UpDirectionSpeed > 0)
            {
                UpDirectionSpeed = 0;
            }
            if (jumpCooldown <= 0 && UpDirectionSpeed > -10 && isJumping && hasLanded)
            {
                ChangeColor(Color.Brown);
                jumpCooldown = 2.0;
                UpDirectionSpeed = JUMP_SPEED;
                hasLanded = false;
            }
            MoveDirection verticalDirection = UpDirectionSpeed < 0 ? MoveDirection.Up : MoveDirection.Down;

            position = new Point(position.X, position.Y + GetVerticalSpeed(verticalDirection));
        }

        public override void Render()
        {
            base.Render();
        }
    }
}
