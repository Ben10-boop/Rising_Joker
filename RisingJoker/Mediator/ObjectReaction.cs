using RisingJoker.BaseGameObjects;
using RisingJoker.CoinObject;
using RisingJoker.PlatformFactory;
using RisingJoker.PointsObserver;
using System.Collections.Generic;
using System.Drawing;

namespace RisingJoker.Mediator
{
    public class ObjectReaction : IMediator
    {
        private Dictionary<Color, PointsCollector> PointsMap;
        public ObjectReaction(Dictionary<Color, PointsCollector> pointsMap)
        {
            PointsMap = pointsMap;
        }
        public void React(IGameObject sender, IGameObject touchedObject)
        {
            if (sender is Coin coin)
            {
                if (touchedObject is Player player)
                {
                    PointsMap[player.info.color].Update(coin.Points, player.info.color.ToString());
                    coin.isAlive = false;
                }
            }

            else if (sender is Enemy enemy)
            {
                if (touchedObject is Player player)
                {
                    PointsMap[player.info.color].Update(enemy.Points, player.info.color.ToString());
                }
            }

            else if (sender is Player player)
            {
                if (touchedObject is Platform platform)
                {
                    player.landPlayer();

                    bool isFalling = player.GetDirectionSpeed(MoveDirection.Down) + player.GetDirectionSpeed(MoveDirection.Up) > 0;
                    Rectangle objBounds = player.GetBounds();
                    Rectangle platformBounds = platform.GetBounds();
                    if (!isFalling)
                    {
                        platform.ChangeColor(Color.BlueViolet);
                        return;
                    }
                    int threshold = 15;
                    bool comingFromTop = objBounds.Bottom >= (platformBounds.Top - 4) && objBounds.Top < platformBounds.Top && objBounds.Bottom - (platformBounds.Top - 4) <= threshold;
                    if (comingFromTop)
                    {
                        platform.ChangeColor(Color.Red);
                        player.MoveBy(new Point(0, platformBounds.Top - objBounds.Bottom - 7));
                    }
                }
            }

        }
    }
}
