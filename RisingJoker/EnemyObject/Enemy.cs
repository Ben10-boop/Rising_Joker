using RisingJoker.BaseGameObjects;
using RisingJoker.PointsObserver;
using System.Collections.Generic;
using System.Drawing;

namespace RisingJoker.PlatformFactory
{
    public class Enemy : MovableObject, IEnemy
    {
        public static string TAG = "enemy";
        private List<IPointsListener> Listeners;
        public int Points { get; }
        public Enemy(Color color, Size size, Point position, int contactPenalty) : base(size, position, true, color, TAG)
        {
            Points = contactPenalty;
            Listeners = new List<IPointsListener>();
        }

        public override void OnCollisionWith(IGameObject other)
        {
            if (other is Player player)
            {
                Notify(player.color.ToString());
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

        public virtual IEnemy Clone()
        {
            return (IEnemy)this.MemberwiseClone();
        }
    }
}
