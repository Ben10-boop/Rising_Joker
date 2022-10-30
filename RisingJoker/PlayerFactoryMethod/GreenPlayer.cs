using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RisingJoker.PlayerFactoryMethod
{
    internal class GreenPlayer : Player
    {
        //Touching players gives points
        private bool TouchingPlayer = false;
        double NextPointGainTime = 0.2;
        public GreenPlayer(Size size, Point position, bool isVisible, Color color) : base(size, position, isVisible, color)
        {

        }
        public override void OnCollisionWith(GameObject other)
        {
            base.OnCollisionWith(other);
            if (other.objectTag == "player")
            {
                TouchingPlayer = true;
            }
        }

        public override int GetUniqueMechanicPoints(double currentGameTime)
        {
            if (TouchingPlayer)
            {
                TouchingPlayer = false;
                if (currentGameTime >= NextPointGainTime)
                {
                    NextPointGainTime = currentGameTime + 0.2;
                    return 8;
                }
            }
            return 0;
        }
    }
}
