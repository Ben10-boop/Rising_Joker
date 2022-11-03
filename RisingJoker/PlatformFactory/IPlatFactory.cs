using System.Drawing;

namespace RisingJoker.PlatformFactory
{
    public enum PlatFactoryType
    {
        Gold,
        Regular,
        Scary
    }
    public interface IPlatFactory
    {
        Coin CreateCoin(int coinSize, int baseCoinValue);
        IEnemy CreateEnemy(Size enemySize, int basePenalty);
        PlatformBottom CreatePlatformBottom(int platformWidth, int platformPosX, int basePenalty);
    }
}
