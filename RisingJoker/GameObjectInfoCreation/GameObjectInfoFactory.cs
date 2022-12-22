using RisingJoker.BaseGameObjects;
using System.Collections.Generic;
using System.Drawing;

namespace RisingJoker.GameObjectInfoCreation
{
    public static class GameObjectInfoFactory
    {
        private static Dictionary<string, GameObjectInfo> gameObjectInfoMap = new Dictionary<string, GameObjectInfo>();

        public static GameObjectInfo GetGameObjectInfo(Size size, Color color, string objectTag)
        {
            GameObjectInfo gameObjectInfo = null;
            gameObjectInfoMap.TryGetValue(size.ToString() + color.ToString() + objectTag, out gameObjectInfo);
            if (gameObjectInfo == null)
            {
                //    Console.WriteLine("ADDING");
                gameObjectInfo = new GameObjectInfo(size, color, objectTag);
                gameObjectInfoMap.Add(size.ToString() + color.ToString() + objectTag, gameObjectInfo);
            }

            return gameObjectInfo;
        }
    }
}
