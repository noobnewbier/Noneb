using System;
using System.Collections.Generic;
using Maps.Repositories.CurrentMapConfig;
using UniRx;
using UnityEngine;
using WorldConfigurations;
using WorldConfigurations.Repositories;

namespace Maps.Services
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
        private readonly ICurrentMapConfigRepository _currentMapConfigRepository;
        private readonly ICurrentWorldConfigRepository _currentWorldConfigRepository;

        public TilesPositionService(ICurrentMapConfigRepository currentMapConfigRepository,
                                    ICurrentWorldConfigRepository currentWorldConfigRepository)
        {
            _currentMapConfigRepository = currentMapConfigRepository;
            _currentWorldConfigRepository = currentWorldConfigRepository;
        }

        public IObservable<IReadOnlyList<Vector3>> GetObservableStream(float yPosition)
        {
            return _currentMapConfigRepository.GetObservableStream()
                .CombineLatest(_currentWorldConfigRepository.GetObservableStream(), (mapConfig, worldConfig) => (mapConfig, worldConfig))
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
            return _currentMapConfigRepository.GetMostRecent()
                .CombineLatest(_currentWorldConfigRepository.GetMostRecent(), (mapConfig, worldConfig) => (mapConfig, worldConfig))
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
            for (var i = 0; i < mapConfig.ZSize; i++)
            {
                var sideOffset = i % 2 * sideDistance / 2f;
                for (var j = 0; j < mapConfig.XSize; j++)
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