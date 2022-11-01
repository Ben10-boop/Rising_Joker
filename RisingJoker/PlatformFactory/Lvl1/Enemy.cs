using RisingJoker.PlatformFactory;
using System.Drawing;

namespace RisingJoker
{
    public class Enemy : MovableObject, IEnemy
    {
        public static string TAG = "enemy";
        private readonly int contactPenalty = -1;
        public Enemy(Color color, Size size, Point position) : base(size, position, true, color, TAG) { }

        public override void OnCollisionWith(GameObject other)
        {
        }

        public int GetContactPenalty()
        {
            return contactPenalty;
        }
    }
}
