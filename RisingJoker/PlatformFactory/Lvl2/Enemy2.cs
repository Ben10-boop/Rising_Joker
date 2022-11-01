﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RisingJoker.PlatformFactory.Lvl2
{
    internal class Enemy2 : Enemy
    {
        private readonly int contactPenalty = -2;
        public Enemy2(Color color, Size size, Point position) : base(color, size, position) { }

        public override void OnCollisionWith(GameObject other)
        {
        }

        public override int GetContactPenalty()
        {
            return contactPenalty;
        }
    }
}
