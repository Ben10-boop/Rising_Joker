using System.Drawing;

namespace RisingJoker.BaseGameObjects
{
    public interface IMovableObject : IGameObject
    {
        int DownDirectionSpeed { get; set; }
        int LeftDirectionSpeed { get; set; }
        int RightDirectionSpeed { get; set; }
        int UpDirectionSpeed { get; set; }
        int ParentXStart { get; set; }
        int ParentXEnd { get; set; }

        int GetDirectionSpeed(MoveDirection direction);
        void Move();
        void MoveBy(Point moveBy);
        void MoveTo(Point moveTo);
    }
}