using System.Collections.Generic;
using Common.Factories;
using Common.Holders;
using Maps;
using Maps.Services;

namespace GameEnvironments.Load.GameObjects
{
    public interface IGameObjectLoadService
    {
        void Load(IReadOnlyList<GameObjectFactory> gameObjectProviders, IReadOnlyList<IBoardItemHolder> holders, MapConfig mapConfig);
    }

    public class GameObjectLoadService : IGameObjectLoadService
    {
        private readonly ICoordinateService _coordinateService;

        public GameObjectLoadService(ICoordinateService coordinateService)
        {
            _coordinateService = coordinateService;
        }

        public void Load(IReadOnlyList<GameObjectFactory> gameObjectProviders, IReadOnlyList<IBoardItemHolder> holders, MapConfig mapConfig)
        {
            foreach (var holder in holders)
            {
                var flattenedIndex = _coordinateService.GetFlattenArrayIndexFromAxialCoordinate(
                    holder.Value.Coordinate.X,
                    holder.Value.Coordinate.Z,
                    mapConfig
                );

                var gameObjectProvider = gameObjectProviders[flattenedIndex];
                if (gameObjectProvider == null) // if there is no gameObject in that coordinate(no board item)
                {
                    continue;
                }

                gameObjectProvider.Create(holder.Transform, false);
            }
        }
    }
}