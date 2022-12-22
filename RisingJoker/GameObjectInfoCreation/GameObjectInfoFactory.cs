using RisingJoker.BaseGameObjects;
using System.Collections.Generic;
using System.Drawing;

namespace RisingJoker.GameObjectInfoCreation
{
    public static class GameObjectInfoFactory
    {
        private static Dictionary<(Size size, Color color, string objectTag), GameObjectInfo> gameObjectInfoMap = new Dictionary<(Size size, Color color, string objectTag), GameObjectInfo>();

        public static GameObjectInfo GetGameObjectInfo(Size size, Color color, string objectTag)
        {
            GameObjectInfo gameObjectInfo;
            gameObjectInfoMap.TryGetValue((size, color, objectTag), out gameObjectInfo);
            if (gameObjectInfo == null)
            {
                gameObjectInfo = new GameObjectInfo(size, color, objectTag);
                gameObjectInfoMap.Add((size, color, objectTag), gameObjectInfo);
            }

            return gameObjectInfo;
        }
    }
}
