using System;
using System.Collections.Generic;
using Noneb.Core.Game.GameState.CurrentMapConfig;
using Noneb.Core.Game.GameState.CurrentWorldConfig;
using Noneb.Core.Game.Maps;
using Noneb.Core.Game.WorldConfigurations;
using UniRx;
using UnityEngine;

namespace Noneb.Ui.Game.Maps.TilesPosition
{
    public interface ITilesPositionService
    {
        //can only calculate the position of tiles when given the y position of the board
        //perhaps we should make further assumption of "everything based on 0,0,0?)
        IObservable<IReadOnlyList<Vector3>> GetObservableStream(float yPosition);
        IObservable<IReadOnlyList<Vector3>> GetMostRecent(float yPosition);
    }

    public class TilesPositionService : ITilesPositionService
    {
        private readonly IMapConfigRepository _mapConfigRepository;
        private readonly IWorldConfigRepository _worldConfigRepository;

        public TilesPositionService(IMapConfigRepository mapConfigRepository,
                                    IWorldConfigRepository worldConfigRepository)
        {
            _mapConfigRepository = mapConfigRepository;
            _worldConfigRepository = worldConfigRepository;
        }

        public IObservable<IReadOnlyList<Vector3>> GetObservableStream(float yPosition)
        {
            return _mapConfigRepository.GetObservableStream()
                .CombineLatest(_worldConfigRepository.GetObservableStream(), (mapConfig, worldConfig) => (mapConfig, worldConfig))
                .Select(
                    tuple =>
                    {
                        var (mapConfig, worldConfig) = tuple;
                        return CreateData(mapConfig, worldConfig, yPosition);
                    }
                );
        }

        public IObservable<IReadOnlyList<Vector3>> GetMostRecent(float yPosition)
        {
            return _mapConfigRepository.GetMostRecent()
                .CombineLatest(_worldConfigRepository.GetMostRecent(), (mapConfig, worldConfig) => (mapConfig, worldConfig))
                .Select(
                    tuple =>
                    {
                        var (mapConfig, worldConfig) = tuple;
                        return CreateData(mapConfig, worldConfig, yPosition);
                    }
                );
        }

        private static IReadOnlyList<Vector3> CreateData(MapConfig mapConfig, WorldConfig worldConfig, float yPosition)
        {
            var toReturn = new List<Vector3>();
            var upDistance = worldConfig.OuterRadius * 1.5f;
            var sideDistance = worldConfig.InnerRadius * 2f;
            for (var i = 0; i < mapConfig.GetMap2DActualHeight(); i++)
            {
                var sideOffset = i % 2 * sideDistance / 2f;
                for (var j = 0; j < mapConfig.GetMap2DActualWidth(); j++)
                    toReturn.Add(
                        new Vector3(
                            j * sideDistance + sideOffset,
                            yPosition,
                            i * upDistance
                        )
                    );
            }
            return toReturn;
        }
    }
}