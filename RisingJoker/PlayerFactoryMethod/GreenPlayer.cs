using RisingJoker.BaseGameObjects;
using RisingJoker.PointsObserver;
using System.Drawing;

namespace RisingJoker.PlayerFactoryMethod
{
    internal class GreenPlayer : Player, IPointsDispatcher
    {
        //Touching players gives points
        private bool TouchingPlayer = false;
        double NextPointGainTime = 0.2;
        public GreenPlayer(Size size, Point position, bool isVisible, Color color) : base(size, position, isVisible, color, 8)
        {

        }
        public override void OnCollisionWith(IGameObject other)
        {
            base.OnCollisionWith(other);
            if (other.info.objectTag == "player" && other != this)
            {
                TouchingPlayer = true;
            }
        }

        protected override void Notify(double currentGameTime)
        {
            if (TouchingPlayer)
            {
                TouchingPlayer = false;
                if (currentGameTime >= NextPointGainTime)
                {
                    NextPointGainTime = currentGameTime + 0.2;
                    Listeners.ForEach((listener) => listener.Update(Points, info.color.ToString()));
                }
            }
        }
    }
}
