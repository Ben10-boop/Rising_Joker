using RisingJoker.BaseGameObjects;
using RisingJoker.PointsObserver;
using System.Collections.Generic;
using System.Drawing;

namespace RisingJoker.PlatformFactory
{
    public class PlatformBottom : MovableObject, IPointsDispatcher
    {
        public static string TAG = "pBottom";
        public int Points { get; }
        private List<IPointsListener> Listeners;
        public PlatformBottom(Size size, Point position, Color color, int passthroughPenalty) : base(size, position, true, color, TAG)
        {
            Points = passthroughPenalty;
            Listeners = new List<IPointsListener>();
        }

        public PlatformBottom(GameObjectInfo info, Point position, int contactPenalty) : base(info, position, true)
        {
            Points = contactPenalty;
            Listeners = new List<IPointsListener>();
        }

        public override void OnCollisionWith(IGameObject other)
        {
            if (other is Player player)
            {
                Notify(player.info.color.ToString());
            }
        }

        private void Notify(string id)
        {
            Listeners.ForEach(listener => listener.Update(Points, id));

        }

        public void Subscribe(IPointsListener listener)
        {
            if (Listeners.Contains(listener))
                return;
            Listeners.Add(listener);
        }

        public void Unsubscribe(IPointsListener listener)
        {
            if (!Listeners.Contains(listener))
                return;
            Listeners.Remove(listener);
        }
    }
}
