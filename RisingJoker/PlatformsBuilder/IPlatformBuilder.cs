using RisingJoker.BaseGameObjects;
using System.Drawing;
using System.Windows.Forms;

namespace RisingJoker.PlatformsBuilder
{
    public interface IPlatformBuilder
    {
        IPlatformBuilder Reset();
        IPlatformBuilder SetSize(Size size);
        IPlatformBuilder SetPosition(Point position);
        IPlatformBuilder SetColor(Color color);
        IPlatformBuilder AddObjToPlatform(IMovableObject obj, bool below = false);
        Platform GetPlatform();
        IPlatformBuilder SetDirectionSpeed(MoveDirection direction, int speed);
    }
}
