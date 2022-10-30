using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RisingJoker.PlayerFactoryMethod
{
    internal class RedPlayerCreator : PlayerCreator
    {
        public override Player CreatePlayer()
        {
            return new RedPlayer(new Size(25, 25), new Point(0, 0), true, Color.Red);
        }
    }
}
