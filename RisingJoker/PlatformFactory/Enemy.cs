using RisingJoker.PlatformFactory;
using System;
using System.Drawing;

namespace RisingJoker
{
    public abstract class Enemy : MovableObject, ICloneable<Enemy>
    {
        public static string TAG = "enemy";
        public Enemy(Color color, Size size, Point position) : base(size, position, true, color, TAG) { }

        public override void OnCollisionWith(GameObject other)
        {
        }

        public virtual int GetContactPenalty()
        {
            return 0;
        }

        public Enemy Clone()
        {
            return (Enemy)this.MemberwiseClone();
        }
    }
}
