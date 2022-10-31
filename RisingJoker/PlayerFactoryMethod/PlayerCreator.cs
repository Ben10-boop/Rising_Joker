using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RisingJoker.PlayerFactoryMethod
{
    public abstract class PlayerCreator
    {
        public virtual Player CreatePlayer()
        {
            return null;
        }
    }
}
