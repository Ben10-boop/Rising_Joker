using System.Drawing;

namespace RisingJoker.PlatformFactory
{
    internal interface IPlatFactory
    {
        Coin CreateCoin(int coinSize, int coinValue);
        Enemy CreateEnemy(Size enemySize, int penalty);
        PlatformBottom CreatePlatformBottom(int platformWidth, int platformPosX, int penalty);
    }
}
