using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RisingJoker.PlayerFactoryMethod
{
    internal class BluePlayer : Player
    {
        //Inreased passive point gain

        double NextPointGainTime = 1;
        public BluePlayer(Size size, Point position, bool isVisible, Color color) : base(size, position, isVisible, color)
        {
            
        }

        public override void UpdateUniqueMechanicPoints(double currentGameTime)
        {
            if(currentGameTime >= NextPointGainTime)
            {
                NextPointGainTime += 1;
                ModifyScore(10);
            }
        }
    }
}
