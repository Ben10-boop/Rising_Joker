using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RisingJoker.PlayerFactoryMethod
{
    internal class GreenPlayerCreator : PlayerCreator
    {
        public override Player CreatePlayer()
        {
            return new GreenPlayer(new Size(25, 25), new Point(0, 0), true, Color.Green);
        }
    }
}
