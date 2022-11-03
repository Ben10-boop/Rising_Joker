using RisingJoker.PointsObserver;
using System.Drawing;

namespace RisingJoker.PlayerFactoryMethod
{
    internal class BluePlayer : Player, IPointsDispatcher
    {
        //Inreased passive point gain

        double NextPointGainTime = 1;

        public BluePlayer(Size size, Point position, bool isVisible, Color color) : base(size, position, isVisible, color, 10)
        {
        }

        protected override void Notify(double currentGameTime)
        {
            if (currentGameTime >= NextPointGainTime)
            {
                NextPointGainTime = currentGameTime + 1;
                Listeners.ForEach((listener) => listener.Update(Points, color.ToString()));
            }
        }
    }
}
