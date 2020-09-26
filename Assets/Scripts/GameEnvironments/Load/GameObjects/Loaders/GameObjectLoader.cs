using System;
using System.Collections.Generic;
using Common;
using Common.Holders;
using Common.Loaders;
using Common.Providers;
using GameEnvironments.Common.Repositories.CurrentLevelDatas;
using Maps;
using Maps.Repositories.CurrentMapConfig;
using UniRx;
using UnityEngine;

namespace GameEnvironments.Load.GameObjects.Loaders
{
    public abstract class GameObjectLoader : ScriptableObject, ILoader
    {
        [SerializeField] private GameObjectLoadServiceProvider serviceProvider;
        [SerializeField] private CurrentLevelDataRepositoryProvider currentLevelDataRepositoryProvider;
        [SerializeField] private CurrentMapConfigRepositoryProvider currentMapConfigRepositoryProvider;

        private IDisposable _disposable;


        [ContextMenu(nameof(LoadAndForget))]
        public void LoadAndForget()
        {
            DisposeDisposables();
            _disposable = GetDataTupleObservable()
                .Subscribe(
                    tuple =>
                    {
                        var (gameObjectProviders, config, holders) = tuple;
                        InvokeLoadService(gameObjectProviders, config, holders);
                    }
                );
        }

        public IObservable<Unit> LoadObservable()
        {
            return GetDataTupleObservable()
                .Select(
                    tuple =>
                    {
                        var (gameObjectProviders, config, holders) = tuple;
                        InvokeLoadService(gameObjectProviders, config, holders);

                        return Unit.Default;
                    }
                );
        }

        private IObservable<(IReadOnlyList<GameObjectProvider> gameObjectProviders, MapConfig config, IReadOnlyList<IBoardItemHolder> holders)>
            GetDataTupleObservable()
        {
            var levelDataRepository = currentLevelDataRepositoryProvider.Provide();
            var mapConfigObservable = currentMapConfigRepositoryProvider.Provide().GetMostRecent();
            var holdersObservable = GetBoardItemsHolderRepository().GetMostRecent();

            return GetGameObjectProvidersFromRepository(levelDataRepository)
                .Zip(
                    mapConfigObservable,
                    holdersObservable,
                    (gameObjectProviders, config, holders) => (gameObjectProviders, config, holders)
                );
        }

        private void InvokeLoadService(IReadOnlyList<GameObjectProvider> gameObjectProviders,
                                       MapConfig config,
                                       IReadOnlyList<IBoardItemHolder> holders)
        {
            var gameObjectLoadService = serviceProvider.Provide();
            gameObjectLoadService.Load(
                gameObjectProviders,
                holders,
                config
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

        protected abstract IObservable<IReadOnlyList<GameObjectProvider>> GetGameObjectProvidersFromRepository(
            ICurrentLevelDataRepository currentLevelDataRepository);

        protected abstract IDataGetRepository<IReadOnlyList<IBoardItemHolder>> GetBoardItemsHolderRepository();
    }
}