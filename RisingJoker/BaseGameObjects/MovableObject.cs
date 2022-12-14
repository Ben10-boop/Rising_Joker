using System.Drawing;

namespace RisingJoker.BaseGameObjects
{
    public enum MoveDirection
    {
        Left, Right, Up, Down
    }

    public abstract class MovableObject : GameObject, IMovableObject
    {
        public virtual int LeftDirectionSpeed { get; set; }
        public virtual int RightDirectionSpeed { get; set; }
        public virtual int UpDirectionSpeed { get; set; }
        public virtual int DownDirectionSpeed { get; set; }
        // Platform as in restriction - window size, platform size, anything that has restrictions
        public virtual int ParentXStart { get; set; }
        public virtual int ParentXEnd { get; set; }


        public MovableObject(Size size, Point position, bool isVisible, Color color, string TAG) : base(size, position, isVisible, color, TAG) { }
        public MovableObject(IGameObjectInfo info, Point position, bool isVisible) : base(info, position, isVisible) { }


        public virtual void Move()
        {
            this.position = new Point(position.X + this.LeftDirectionSpeed + this.RightDirectionSpeed, position.Y + this.UpDirectionSpeed + this.DownDirectionSpeed);
        }

        public virtual void MoveBy(Point moveBy)
        {
            this.position = new Point(position.X + moveBy.X, position.Y + moveBy.Y);
        }

        public virtual void MoveTo(Point moveTo)
        {
            this.position = new Point(moveTo.X, moveTo.Y);

        }

        public virtual int GetDirectionSpeed(MoveDirection direction)
        {
            switch (direction)
            {
                case MoveDirection.Up:
                    return UpDirectionSpeed;
                case MoveDirection.Down:
                    return DownDirectionSpeed;
                case MoveDirection.Right:
                    return RightDirectionSpeed;
                case MoveDirection.Left:
                    return LeftDirectionSpeed;
                default:
                    return 0;
            }
        }
    }
}
