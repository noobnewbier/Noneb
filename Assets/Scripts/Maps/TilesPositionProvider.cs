using System;
using System.Collections.Generic;
using Common.Providers;
using Maps.Repositories;
using UniRx;
using UnityEngine;
using WorldConfigurations;
using WorldConfigurations.Repositories;

namespace Maps
{
    //we probably want a way to "fix" our order of storage for tiles...
    public class TilesPositionProvider : MonoObjectProvider<IList<Vector3>>
    {
        [SerializeField] private MapConfigurationRepositoryProvider mapConfigurationRepositoryProvider;
        [SerializeField] private WorldConfigurationRepositoryProvider worldConfigurationRepositoryProvider;

        private IDisposable _disposable;
        private IList<Vector3> _cache;
        private MapConfiguration _currentMapConfig;
        private WorldConfiguration _currentWorldConfig;


        private void OnEnable()
        {
            var worldConfigObservable = worldConfigurationRepositoryProvider.Provide().Get();
            var mapConfigObservable = mapConfigurationRepositoryProvider.Provide().Get();

            _disposable = mapConfigObservable
                .Zip(worldConfigObservable, (mapConfig, worldConfig) => (mapConfig, worldConfig))
                .Subscribe(
                    tuple =>
                    {
                        var (mapConfig, worldConfig) = tuple;
                        _cache = CreateCache(mapConfig, worldConfig);
                    }
                );
        }

        public override IList<Vector3> Provide()
        {
            if (_cache == null)
            {
                throw new InvalidOperationException($"{nameof(_cache)} is null, you probably haven't provide a proper map config yet");
            }

            return _cache;
        }

        private IList<Vector3> CreateCache(MapConfiguration mapConfig, WorldConfiguration worldConfig)
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
                            transform.position.y,
                            i * upDistance
                        )
                    );
            }
            return toReturn;
        }

        private void OnDisable()
        {
            _disposable?.Dispose();
        }
    }
}