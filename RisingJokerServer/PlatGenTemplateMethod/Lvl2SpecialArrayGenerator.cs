using RisingJokerServer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RisingJokerServer.PlatGenTemplateMethod
{
    internal sealed class Lvl2SpecialArrayGenerator : PlatformGenerator
    {
        private Lvl2ArrayGenerator Generator = new Lvl2ArrayGenerator();

        public override PlatformDto GeneratePlatform()
        {
            PlatformDto platform =  Generator.GeneratePlatform();
            platform.PlatformAmount = 1;
            platform.HasCoin = true;
            platform.HasEnemy = true;
            return platform;
        }
    }
}
