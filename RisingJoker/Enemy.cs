using System.Drawing;

namespace RisingJoker
{
    public class Enemy : MovableObject
    {
        public static string TAG = "enemy";
        public Enemy(Color color, Size size, Point position) : base(size, position, true, color, TAG) { }

        public override void OnCollision(GameObject other)
        {
            throw new System.NotImplementedException();
        }
    }
}
