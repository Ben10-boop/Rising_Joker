using RisingJoker.Object;
using System.Drawing;

namespace RisingJoker.EnemyObject
{
    public class Enemy : MovableObject, IEnemy
    {
        public static string TAG = "enemy";
        public Enemy(Color color, Size size, Point position) : base(size, position, true, color, TAG) { }
    }
}
