using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RisingJoker.PlatformsBuilder
{
    public interface IPlatformsBuilder
    {
        IPlatformsBuilder Reset();
        IPlatformsBuilder SetSize(Size size);
        IPlatformsBuilder SetPosition(Point position);
        IPlatformsBuilder SetColor(Color color);
        IPlatformsBuilder AddCoin(Coin coin, Label form);
        IPlatformsBuilder AddBottom(PlatformBottom bottom, Label form);
        List<Platform> GetPlatform();
        IPlatformsBuilder SetDirectionSpeed(MoveDirection direction, int speed);
        IPlatformsBuilder AddEnemy(Enemy enemy, Label form);
    }
}
