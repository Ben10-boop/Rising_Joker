using System.Drawing;

namespace RisingJoker.BaseGameObjects
{
    public interface IGameObjectInfo
    {
        Size size { get; set; }
        Color color { get; set; }
        string objectTag { get; set; }
    }
}
