using System.Collections.Generic;
using Common.Holders;
using Common.Providers;

namespace GameEnvironments.Load.GameObjects
{
    public interface IGameObjectLoadService
    {
        void Load(IReadOnlyList<GameObjectProvider> gameObjectProviders, IReadOnlyList<IBoardItemHolder> holders, int mapXSize, int mapZSize);
    }

    public class GameObjectLoadService : IGameObjectLoadService
    {
        public void Load(IReadOnlyList<GameObjectProvider> gameObjectProviders, IReadOnlyList<IBoardItemHolder> holders, int mapXSize, int mapZSize)
        {
            for (var i = 0; i < mapZSize; i++)
            for (var j = 0; j < mapXSize; j++)
            {
                var index = i * mapXSize + j;
                var gameObjectProvider = gameObjectProviders[index];
                if (gameObjectProvider == null) // if there is no gameObject in that coordinate(no board item)
                {
                    continue;
                }

                gameObjectProvider.Provide(holders[index].Transform, false);
            }
        }
    }
}