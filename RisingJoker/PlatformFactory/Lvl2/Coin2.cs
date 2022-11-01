﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RisingJoker.PlatformFactory.Lvl2
{
    public class Coin2 : Coin
    {
        private readonly int Value = 75;
        public Coin2(Color color, Size size, Point position) : base(color, size, position) { }

        public override void OnCollisionWith(GameObject other)
        {
        }

        public override int GetValue()
        {
            return Value;
        }
    }
}
