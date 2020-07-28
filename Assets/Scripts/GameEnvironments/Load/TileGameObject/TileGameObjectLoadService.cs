using System.Collections.Generic;
using Common.Providers;
using UnityEngine;

namespace GameEnvironments.Load.TileGameObject
{
    public interface ITileGameObjectLoadService
    {
        void Load(GameObjectProvider[] gameObjectProviders, IList<Transform> tileTransforms, int mapXSize, int mapZSize);
    }

    public class TileGameObjectLoadService : ITileGameObjectLoadService
    {
        public void Load(GameObjectProvider[] gameObjectProviders, IList<Transform> tileTransforms, int mapXSize, int mapZSize)
        {
            for (var i = 0; i < mapZSize; i++)
            for (var j = 0; j < mapXSize; j++)
            {
                var index = i * mapXSize + j;
                var gameObject = gameObjectProviders[index].Provide();
                gameObject.transform.parent = tileTransforms[index];
                //The parent's(holder) transform will handle both rotation and position for us
                gameObject.transform.localPosition = Vector3.zero;
                gameObject.transform.localRotation = Quaternion.identity;
                gameObject.transform.localScale = Vector3.one;
            }
        }
    }
}