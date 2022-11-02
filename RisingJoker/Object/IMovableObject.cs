using System.Drawing;

namespace RisingJoker.Object
{
    public interface IMovableObject : IGameObject
    {
        int DownDirectionSpeed { get; set; }
        int LeftDirectionSpeed { get; set; }
        int RightDirectionSpeed { get; set; }
        int UpDirectionSpeed { get; set; }
        int XEnd { get; set; }
        int XStart { get; set; }

        int GetDirectionSpeed(MoveDirection direction);
        void Move();
        void MoveBy(Point moveBy);
        void MoveTo(Point moveTo);
    }
}