using System.Collections.Generic;
using Main.Core.Game.Common.Factories;
using Main.Core.Game.Maps;
using Main.Core.Game.Maps.Coordinate.Services;
using Main.Ui.Game.Common.Holders;

namespace Main.Ui.Game.GameEnvironments.Load.GameObjects
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