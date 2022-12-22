using System.Drawing;

namespace RisingJoker.BaseGameObjects
{
    public class GameObjectInfo : IGameObjectInfo
    {
        public Size size { get; set; }
        public Color color { get; set; }
        public string objectTag { get; set; }

        public GameObjectInfo(Size size, Color color, string objectTag)
        {
            this.size = size;
            this.color = color;
            this.objectTag = objectTag;
        }
    }
}
