using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RisingJoker.PlatformFactory.Lvl2
{
    internal class Enemy2 : MovableObject, IEnemy
    {
        public static string TAG = "enemy";
        private readonly int contactPenalty = -2;
        public Enemy2(Color color, Size size, Point position) : base(size, position, true, color, TAG) { }

        public override void OnCollisionWith(GameObject other)
        {
        }

        public int GetContactPenalty()
        {
            return contactPenalty;
        }
    }
}
