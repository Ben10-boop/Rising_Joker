﻿using RisingJoker.PlatformFactory;
using System;
using System.Drawing;

namespace RisingJoker
{
    public abstract class Coin : MovableObject, ICloneable<Coin>
    {
        public static string TAG = "coin";
        public Coin(Color color, Size size, Point position) : base(size, position, true, color, TAG) { }

        public override void OnCollisionWith(GameObject other)
        {
        }

        public virtual int GetValue()
        {
            return 0;
        }

        public Coin Clone()
        {
            return (Coin)MemberwiseClone();
        }
    }
}
