using RisingJoker.BaseGameObjects;
using RisingJoker.EnemyObject;
using RisingJoker.PlatformFactory;
using RisingJoker.PlatformsBuilder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public PlatformObjAddFacade(List<IGameObject> gameObjects, IPlatformBuilder platformBuilder, Coin coin, IEnemy enemy, IPlatFactory platFactory, int level, int platformWidth)
        {
            this.gameObjects = gameObjects;
            this.platformBuilder = platformBuilder;
            this.coin = coin;
            this.enemy = enemy;
            this.platFactory = platFactory;
            this.level = level;
            this.platformWidth = platformWidth;
        }

        public void AddObject(int moveBy, PlatformObjectType objectType)
        {
            switch (objectType)
            {
                case (PlatformObjectType.platformBottom):
                    PlatformBottom bottom = platFactory.CreatePlatformBottom(platformWidth, moveBy, -(int)Math.Log(10 * Math.Pow(level, 2)));
                    gameObjects.Add(bottom);
                    platformBuilder.AddObjToPlatform(bottom, true);
                    break;
                case (PlatformObjectType.coin):
                    Coin clonedCoin = coin.Clone();
                    clonedCoin.MoveBy(new Point(moveBy, 0));
                    platformBuilder.AddObjToPlatform(clonedCoin);
                    gameObjects.Add(clonedCoin);
                    break;
                case (PlatformObjectType.enemy):
                    IEnemy clonedEnemy = enemy.Clone();
                    clonedEnemy.MoveBy(new Point(moveBy, 0));
                    platformBuilder.AddObjToPlatform(clonedEnemy);
                    gameObjects.Add(clonedEnemy);
                    break;
                default:
                    throw new Exception();
            }
        }
    }
}
