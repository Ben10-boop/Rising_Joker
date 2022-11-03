using RisingJoker.PlatformFactory;
using RisingJoker.PlatformsBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RisingJoker
{
    public enum PlatformType
    {
        Gold,
        Regular,
        Scary
    }

    public class PlatformObjPickFacade
    {

        public PlatformObjPickFacade()
        {
        }

        public (IPlatFactory, PlatFactoryType) PickPlatform(bool hasCoin, bool hasEnemy, PlatformColorTheme theme)
        {
            if (hasCoin && !hasEnemy)
            {
                return (new GoldPlatform(), PlatFactoryType.Gold);
            }
            else if (!hasCoin && hasEnemy)
            {
                return (new ScaryPlatform(), PlatFactoryType.Scary);
            }
            else
            {
                return (new RegularPlatform(theme), PlatFactoryType.Regular);
            }
        }
    }
}
