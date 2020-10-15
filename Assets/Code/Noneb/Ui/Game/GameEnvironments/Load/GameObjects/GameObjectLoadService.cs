using System.Collections.Generic;
using Noneb.Core.Game.Common.Factories;
using Noneb.Core.Game.Coordinates;
using Noneb.Core.Game.Maps;
using Noneb.Ui.Game.Common.Holders;

namespace Noneb.Ui.Game.GameEnvironments.Load.GameObjects
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