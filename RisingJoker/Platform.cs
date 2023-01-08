using System.Drawing;

namespace RisingJoker
{
    public class Platform : GameObject
    {
        public static string TAG = "platform";

        public Platform(Size size, Point position, Color color) : base(size, position, true, color, TAG)
        {
        }

        public override void OnCollisionWith(GameObject obj)
        {
            if (obj.objectTag != "player")
            {
                return;
            }

            bool isFalling = obj.GetDirectionSpeed(MoveDirection.Down) + obj.GetDirectionSpeed(MoveDirection.Up) > 0;
            if (!isFalling)
            {
                return;
            }
            Rectangle objBounds = obj.GetBounds();
            Rectangle platformBounds = GetBounds();

            int threshold = 15;
            bool comingFromTop = objBounds.Bottom >= (platformBounds.Top - 4) && objBounds.Top < platformBounds.Top && objBounds.Bottom - (platformBounds.Top - 4) <= threshold;
            if (comingFromTop)
            {
                obj.MoveBy(new Point(0, platformBounds.Top - objBounds.Bottom - 7));
            }

        }
    }
}
