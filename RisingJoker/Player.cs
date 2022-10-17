using System;
using System.Drawing;

namespace RisingJoker
{
    public class Player : MovableObject
    {
        public static string TAG = "player";
        const int HORIZONTAL_SPEED = 7;
        const int FALL_DOWN_SPEED = 10;
        const int JUMP_SPEED = -FALL_DOWN_SPEED - 20;
        private double jumpCooldown = 0;
        private bool isJumping, isMovingLeft, isMovingRight = false;

        public Player(Size size, Point position, bool isVisible, Color color) : base(size, position, isVisible, color, TAG)
        {
            this.LeftDirectionSpeed = -HORIZONTAL_SPEED;
            this.RightDirectionSpeed = HORIZONTAL_SPEED;
            this.UpDirectionSpeed = 0;
            this.DownDirectionSpeed = FALL_DOWN_SPEED;
        }

        public override void OnCollision(GameObject other)
        {
            throw new NotImplementedException();
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
                    return this.DownDirectionSpeed + this.UpDirectionSpeed;
                default:
                    return this.DownDirectionSpeed;
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
                this.jumpCooldown -= 2.0 / 20;
                this.ChangeColor(Color.IndianRed);
            }
            if (this.UpDirectionSpeed < 0)
            {
                this.UpDirectionSpeed -= JUMP_SPEED / 20;
            }
            if (this.UpDirectionSpeed >= 0)
            {
                this.UpDirectionSpeed = 0;
            }
            if (jumpCooldown <= 0 && this.UpDirectionSpeed == 0 && isJumping && IsCollidingWith(Platform.TAG))
            {
                this.jumpCooldown = 2.0;
                this.UpDirectionSpeed = JUMP_SPEED;
            }

            MoveDirection verticalDirection = isJumping ? MoveDirection.Up : MoveDirection.Down;
            position = new Point(position.X, position.Y + GetVerticalSpeed(verticalDirection));
        }
    }
}
