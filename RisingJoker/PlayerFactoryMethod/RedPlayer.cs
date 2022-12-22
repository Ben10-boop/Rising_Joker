using RisingJoker.BaseGameObjects;
using RisingJoker.PointsObserver;
using System.Drawing;

namespace RisingJoker.PlayerFactoryMethod
{
    internal class RedPlayer : Player, IPointsDispatcher
    {
        //Touching enemies gives points
        private bool TouchingEnemy = false;
        double NextPointGainTime = 0.2;

        public RedPlayer(Size size, Point position, bool isVisible, Color color) : base(size, position, isVisible, color, 12)
        {
        }
        public override void OnCollisionWith(IGameObject other)
        {
            base.OnCollisionWith(other);
            if (other.info.objectTag == "enemy")
            {
                TouchingEnemy = true;
            }
        }

        protected override void Notify(double currentGameTime)
        {
            if (TouchingEnemy)
            {
                TouchingEnemy = false;
                if (currentGameTime >= NextPointGainTime)
                {
                    NextPointGainTime = currentGameTime + 0.2;
                    Listeners.ForEach((listener) => listener.Update(Points, info.color.ToString()));
                }
            }
        }
    }
}
