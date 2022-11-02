using RisingJoker.BaseGameObjects;
using System.Drawing;

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
        public override void OnCollisionWith(IGameObject other)
        {
            base.OnCollisionWith(other);
            if (other.objectTag == "player")
            {
                TouchingPlayer = true;
            }
        }

        public override void UpdateUniqueMechanicPoints(double currentGameTime)
        {
            if (TouchingPlayer)
            {
                TouchingPlayer = false;
                if (currentGameTime >= NextPointGainTime)
                {
                    NextPointGainTime = currentGameTime + 0.2;
                    ModifyScore(8);
                }
            }
        }
    }
}
