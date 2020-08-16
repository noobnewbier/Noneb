using System;
using System.Collections.Immutable;
using Common.Loaders;
using Common.Providers;
using GameEnvironments.Common.Repositories.CurrentLevelData;
using Maps;
using Maps.Repositories;
using Tiles;
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

        [SerializeField] private TilesTransformProvider tilesTransformProvider;
        [SerializeField] private MapConfigurationRepositoryProvider mapConfigurationRepositoryProvider;

        private IDisposable _disposable;


        [ContextMenu(nameof(LoadAndForget))]
        public void LoadAndForget()
        {
            DisposeDisposables();
            _disposable = GetDataTupleObservable()
                .Subscribe(
                    tuple =>
                    {
                        var (gameObjectProviders, config) = tuple;
                        InvokeLoadService(gameObjectProviders, config);
                    }
                );
        }

        public IObservable<Unit> LoadObservable()
        {
            return GetDataTupleObservable()
                .Select(
                    tuple =>
                    {
                        var (gameObjectProviders, config) = tuple;
                        InvokeLoadService(gameObjectProviders, config);

                        return Unit.Default;
                    }
                );
        }

        private IObservable<(ImmutableArray<GameObjectProvider> gameObjectProviders, MapConfiguration config)> GetDataTupleObservable()
        {
            var levelDataRepository = currentLevelDataRepositoryProvider.Provide();
            var mapConfigObservable = mapConfigurationRepositoryProvider.Provide().Get();

            return GetGameObjectProvidersFromRepository(levelDataRepository)
                .Zip(mapConfigObservable, (gameObjectProviders, config) => (gameObjectProviders, config))
                .Take(1);
        }

        private void InvokeLoadService(ImmutableArray<GameObjectProvider> gameObjectProviders, MapConfiguration config)
        {
            var gameObjectLoadService = serviceProvider.Provide();
            gameObjectLoadService.Load(
                gameObjectProviders,
                tilesTransformProvider.Provide(),
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