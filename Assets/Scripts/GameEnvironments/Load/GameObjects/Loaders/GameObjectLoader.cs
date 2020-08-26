using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Common.Loaders;
using Common.Providers;
using GameEnvironments.Common.Repositories.CurrentLevelData;
using Maps;
using Maps.Repositories;
using Maps.Services;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameEnvironments.Load.GameObjects.Loaders
{
    public abstract class GameObjectLoader : MonoBehaviour, ILoader
    {
        [SerializeField] private GameObjectLoadServiceProvider serviceProvider;

        [FormerlySerializedAs("levelDataRepositoryProvider")] [SerializeField]
        private CurrentLevelDataRepositoryProvider currentLevelDataRepositoryProvider;

        [SerializeField] private CurrentTilesTransformRepositoryProvider currentTilesTransformRepositoryProvider;

        [FormerlySerializedAs("mapConfigurationRepositoryProvider")] [SerializeField]
        private CurrentMapConfigRepositoryProvider currentMapConfigRepositoryProvider;

        private ICurrentTilesTransformGetRepository _currentTilesTransformGetRepository;
        private IDisposable _disposable;


        [ContextMenu(nameof(LoadAndForget))]
        public void LoadAndForget()
        {
            DisposeDisposables();
            _disposable = GetDataTupleObservable()
                .Subscribe(
                    tuple =>
                    {
                        var (gameObjectProviders, config, tilesTransform) = tuple;
                        InvokeLoadService(gameObjectProviders, config, tilesTransform);
                    }
                );
        }

        public IObservable<Unit> LoadObservable()
        {
            return GetDataTupleObservable()
                .Select(
                    tuple =>
                    {
                        var (gameObjectProviders, config, tilesTransform) = tuple;
                        InvokeLoadService(gameObjectProviders, config, tilesTransform);

                        return Unit.Default;
                    }
                );
        }

        private IObservable<(ImmutableArray<GameObjectProvider> gameObjectProviders,
            MapConfig config,
            IList<Transform> tilesTransform)> GetDataTupleObservable()
        {
            var levelDataRepository = currentLevelDataRepositoryProvider.Provide();
            var mapConfigObservable = currentMapConfigRepositoryProvider.Provide().GetMostRecent();
            var tilesTransformProviderObservable = currentTilesTransformRepositoryProvider.Provide().GetMostRecent();

            return GetGameObjectProvidersFromRepository(levelDataRepository)
                .Zip(
                    mapConfigObservable,
                    tilesTransformProviderObservable,
                    (gameObjectProviders, config, tilesTransform) => (gameObjectProviders, config, tilesTransform)
                );
        }

        private void InvokeLoadService(ImmutableArray<GameObjectProvider> gameObjectProviders, MapConfig config, IList<Transform> tilesTransform)
        {
            var gameObjectLoadService = serviceProvider.Provide();
            gameObjectLoadService.Load(
                gameObjectProviders,
                tilesTransform,
                config.GetMap2DActualWidth(),
                config.GetMap2DActualHeight()
            );
        }


        private void DisposeDisposables()
        {
            _disposable?.Dispose();
        }

        private void OnDisable()
        {
            DisposeDisposables();
        }

        protected abstract IObservable<ImmutableArray<GameObjectProvider>> GetGameObjectProvidersFromRepository(
            ICurrentLevelDataRepository currentLevelDataRepository);
    }
}