using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RisingJokerServer.PlatGenTemplateMethod
{
    internal sealed class Lvl2PlatGenerator : PlatformGenerator
    {
        protected override void GenerateVariables(out int width, out int height, out int position, out int type, out int platAmount)
        {
            var rand = new Random();
            width = rand.Next(175, 276);
            height = rand.Next(15, 21);
            position = rand.Next(0, 501 - width);
            type = rand.Next(0, 4);
            platAmount = 1;
        }
    }
}
