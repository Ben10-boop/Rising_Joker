using RisingJoker.PlatformFactory;
using RisingJoker.PlatformsBuilder;

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

        public (IPlatFactory, PlatFactoryType) PickPlatform(bool hasCoin, bool hasEnemy, PlatformColorTheme theme, int coinPos, int enemyPos)
        {
            bool hasBoth = hasCoin && hasEnemy;
            if ((hasCoin && !hasEnemy) || (hasBoth && enemyPos - coinPos > 0))
            {
                return (new GoldPlatform(), PlatFactoryType.Gold);
            }
            else if ((!hasCoin && hasEnemy) || (hasBoth && enemyPos - coinPos <= 0))
            {
                return (new ScaryPlatform(), PlatFactoryType.Scary);
            }
            return (new RegularPlatform(theme), PlatFactoryType.Regular);
        }
    }
}
