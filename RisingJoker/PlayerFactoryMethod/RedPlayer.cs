using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RisingJoker.PlayerFactoryMethod
{
    internal class RedPlayer : Player
    {
        //Touching enemies gives points
        private bool TouchingEnemy = false;
        double NextPointGainTime = 0.2;
        public RedPlayer(Size size, Point position, bool isVisible, Color color) : base(size, position, isVisible, color)
        {

        }
        public override void OnCollisionWith(GameObject other)
        {
            base.OnCollisionWith(other);
            if (other.objectTag == "enemy")
            {
                TouchingEnemy = true;
            }
        }

        public override void UpdateUniqueMechanicPoints(double currentGameTime)
        {
            if (TouchingEnemy)
            {
                TouchingEnemy = false;
                if (currentGameTime >= NextPointGainTime)
                {
                    NextPointGainTime = currentGameTime + 0.2;
                    ModifyScore(12);
                }
            }
        }
    }
}
