using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RisingJoker.PlatformFactory.Lvl2
{
    public class Coin2 : MovableObject, ICoin
    {
        public static string TAG = "coin";
        private readonly int Value = 75;
        public Coin2(Color color, Size size, Point position) : base(size, position, true, color, TAG) { }

        public override void OnCollisionWith(GameObject other)
        {
        }

        public int GetValue()
        {
            return Value;
        }
    }
}
