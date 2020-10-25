using System;
using System.Collections.Generic;
using Noneb.Core.Game.Common.Factories;
using Noneb.Core.Game.Common.Loaders;
using Noneb.Core.Game.GameState.LevelDatas;
using Noneb.Core.Game.GameState.MapConfig;
using Noneb.Core.Game.Maps;
using Noneb.Ui.Game.Common.Holders;
using Noneb.Ui.Game.GameEnvironments.BoardItemsHoldersFetchingService;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;

namespace Noneb.Ui.Game.GameEnvironments.Load.GameObjects.Loaders
{
    public abstract class GameObjectLoader : ScriptableObject, ILoader
    {
        [SerializeField] private GameObjectLoadServiceProvider serviceProvider;
        [FormerlySerializedAs("currentLevelDataRepositoryProvider")] [SerializeField] private LevelDataRepositoryProvider selectedLevelDataRepositoryProvider;
        [FormerlySerializedAs("currentMapConfigRepositoryProvider")] [SerializeField] private MapConfigRepositoryProvider selectedMapConfigRepositoryProvider;

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

        private IObservable<(IReadOnlyList<GameObjectFactory> gameObjectProviders, MapConfig config, IReadOnlyList<IBoardItemHolder> holders)>
            GetDataTupleObservable()
        {
            var levelDataRepository = selectedLevelDataRepositoryProvider.Provide();
            var mapConfigObservable = selectedMapConfigRepositoryProvider.Provide().GetMostRecent();
            var holdersObservable = GetBoardItemsHolderFetchingService().Fetch();

            return GetGameObjectProvidersFromRepository(levelDataRepository)
                .Zip(
                    mapConfigObservable,
                    holdersObservable,
                    (gameObjectProviders, config, holders) => (gameObjectProviders, config, holders)
                );
        }

        private void InvokeLoadService(IReadOnlyList<GameObjectFactory> gameObjectProviders,
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

        protected abstract IObservable<IReadOnlyList<GameObjectFactory>> GetGameObjectProvidersFromRepository(
            ILevelDataRepository levelDataRepository);

        protected abstract IBoardItemHoldersFetchingService<IBoardItemHolder> GetBoardItemsHolderFetchingService();
    }
}