using RisingJoker.CoinObject;
using RisingJoker.GameObjectInfoCreation;
using System.Drawing;

namespace RisingJoker.PlatformFactory
{
    public class GoldCoin : Coin
    {

        public GoldCoin(Size size, Point position, int coinValue) : base(GameObjectInfoFactory.GetGameObjectInfo(size, Color.Gold, Coin.TAG), position, coinValue + 5)
        {
        }
    }
}
