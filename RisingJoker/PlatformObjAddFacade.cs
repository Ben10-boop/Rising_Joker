using RisingJoker.BaseGameObjects;
using RisingJoker.CoinObject;
using RisingJoker.PlatformFactory;
using RisingJoker.PlatformsBuilder;
using RisingJoker.PointsObserver;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace RisingJoker
{
    public enum PlatformObjectType
    {
        platformBottom,
        coin,
        enemy
    }

    public class PlatformObjAddFacade
    {
        List<IGameObject> gameObjects;
        IPlatformBuilder platformBuilder;
        IPlatFactory platFactory;
        Coin coin;
        IEnemy enemy;
        int level;
        int platformWidth;
        Dictionary<Color, PointsCollector> pointsCollectorsMap;

        public PlatformObjAddFacade(List<IGameObject> gameObjects, IPlatformBuilder platformBuilder, Coin coin, IEnemy enemy, IPlatFactory platFactory, int level, int platformWidth, Dictionary<Color, PointsCollector> pointsCollectorsMap)
        {
            this.gameObjects = gameObjects;
            this.platformBuilder = platformBuilder;
            this.coin = coin;
            this.enemy = enemy;
            this.platFactory = platFactory;
            this.level = level;
            this.platformWidth = platformWidth;
            this.pointsCollectorsMap = pointsCollectorsMap;
        }

        public void AddObject(int moveBy, PlatformObjectType objectType)
        {
            switch (objectType)
            {
                case (PlatformObjectType.platformBottom):
                    PlatformBottom bottom = platFactory.CreatePlatformBottom(platformWidth, moveBy, -(int)Math.Log(3 * Math.Pow(level, 2)));
                    gameObjects.Add(bottom);
                    platformBuilder.AddObjToPlatform(bottom, true);
                    subscribe(bottom);

                    break;
                case (PlatformObjectType.coin):
                    Coin clonedCoin = coin.Clone();
                    clonedCoin.MoveBy(new Point(moveBy, 0));
                    platformBuilder.AddObjToPlatform(clonedCoin);
                    gameObjects.Add(clonedCoin);
                    subscribe(clonedCoin);
                    break;
                case (PlatformObjectType.enemy):
                    IEnemy clonedEnemy = enemy.Clone();
                    clonedEnemy.MoveBy(new Point(moveBy, 0));
                    platformBuilder.AddObjToPlatform(clonedEnemy);
                    gameObjects.Add(clonedEnemy);
                    subscribe(clonedEnemy);
                    break;
                default:
                    throw new Exception();
            }
        }

        private void subscribe(IPointsDispatcher pointsDispatcher)
        {
            pointsCollectorsMap.Values.ToList().ForEach((listener) => pointsDispatcher.Subscribe(listener));
        }
    }
}
