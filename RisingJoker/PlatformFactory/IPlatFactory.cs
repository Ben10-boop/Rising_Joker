﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RisingJoker.PlatformFactory
{
    internal interface IPlatFactory
    {
        Coin CreateCoin();
        Enemy CreateEnemy();
        PlatformBottom CreatePlatformBottom(int platformWidth, int platformPosX);
    }
}
