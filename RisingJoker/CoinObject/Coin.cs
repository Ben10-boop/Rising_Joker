using RisingJoker.BaseGameObjects;
using RisingJoker.PointsObserver;
using System.Collections.Generic;
using System.Drawing;

namespace RisingJoker.CoinObject
{
    public class Coin : MovableObject, ICoin
    {
        public static string TAG = "coin";
        public int Points { get; }
        public List<IPointsListener> Listeners { get; }

        public Coin(Color color, Size size, Point position, int coinValue) : base(size, position, true, color, TAG)
        {
            Points = coinValue;
            Listeners = new List<IPointsListener>();
        }

        public Coin(GameObjectInfo info, Point position, int contactPenalty) : base(info, position, true)
        {
            Points = contactPenalty;
            Listeners = new List<IPointsListener>();
        }

        public Coin Clone()
        {
            return (Coin)MemberwiseClone();
        }

        public override void OnCollisionWith(IGameObject other)
        {
            if (mediator != null)
            {
                mediator.React(this, other);
            }
        }

        public void Notify(string id)
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
