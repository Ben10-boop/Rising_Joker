using RisingJoker.BaseGameObjects;
using System.Drawing;

namespace RisingJoker.PlatformFactory
{
    public class Enemy : MovableObject, IEnemy
    {
        public static string TAG = "enemy";
        public int Points { get; }
        public Enemy(Color color, Size size, Point position, int contactPenalty) : base(size, position, true, color, TAG)
        {
            Points = contactPenalty;
        }

        public virtual IEnemy Clone()
        {
            return (IEnemy)this.MemberwiseClone();
        }
    }
}
