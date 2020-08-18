using System;
using System.Linq;
using Common.Providers;
using Maps;
using Maps.Repositories;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;

namespace Tiles.Holders.Repository
{
    /// <summary>
    /// This is a bit convoluted as we want to be both lazy about the initialization, while keeping everything up to date
    /// in the long term we probably need to refactor the whole provider thingy, they are not so straightforward to use
    /// once you include async operation(which has its pros and cons, to think)
    /// </summary>
    public class TileHolderRepositoryProvider : MonoObjectProvider<ITileHoldersRepository>
    {
        [SerializeField] private TilesTransformProvider tilesTransformProvider;
        [FormerlySerializedAs("mapConfigurationRepositoryProvider")] [SerializeField] private CurrentMapConfigRepositoryProvider currentMapConfigRepositoryProvider;

        private ITileHoldersRepository _cache;
        private MapConfig _currentMapConfig;
        private bool _isCacheUpToDate;
        private ICurrentMapConfigRepository _currentMapConfigRepository;
        private IDisposable _disposable;


        private void OnEnable()
        {
            _currentMapConfigRepository = currentMapConfigRepositoryProvider.Provide();
            _disposable = _currentMapConfigRepository.GetObservableStream()
                .Subscribe(
                    config =>
                    {
                        _currentMapConfig = config;
                        _isCacheUpToDate = false;
                    }
                );
        }

        public override ITileHoldersRepository Provide()
        {
            if (!_isCacheUpToDate)
            {
                _cache = CreateTileHoldersRepository();
            }

            return _cache;
        }

        private ITileHoldersRepository CreateTileHoldersRepository()
        {
            if (_currentMapConfig == null)
            {
                throw new InvalidOperationException($"{nameof(_currentMapConfig)} is null, you probably haven't provide a proper map config yet");
            }

            var representations = tilesTransformProvider.Provide().Select(t => t.GetComponent<TileHolder>()).ToList();
            return new TileHoldersRepository(representations, _currentMapConfig.XSize, _currentMapConfig.ZSize);
        }

        private void OnDisable()
        {
            _disposable?.Dispose();
        }
    }
}