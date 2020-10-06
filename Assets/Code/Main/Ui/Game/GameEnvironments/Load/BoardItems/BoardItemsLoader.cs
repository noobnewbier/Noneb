using System;
using System.Collections.Immutable;
using Main.Core.Game.Common.BoardItems;
using Main.Core.Game.Common.Loaders;
using Main.Core.Game.GameEnvironments.CurrentLevelDatas;
using Main.Core.Game.GameEnvironments.Load;
using Main.Core.Game.Maps;
using Main.Core.Game.Maps.CurrentMapConfig;
using UniRx;
using UnityEngine;

namespace Main.Ui.Game.GameEnvironments.Load.BoardItems
{
    public abstract class BoardItemsLoader<TData> : ScriptableObject, ILoader where TData : BoardItemData
    {
        [SerializeField] private CurrentLevelDataRepositoryProvider currentLevelDataRepositoryProvider;
        [SerializeField] private CurrentMapConfigRepositoryProvider currentMapConfigRepositoryProvider;

        private IDisposable _disposable;

        public void LoadAndForget()
        {
            DisposeDisposables();
            _disposable = GetDataTupleObservable()
                .Subscribe(
                    tuple =>
                    {
                        var (datas, config) = tuple;
                        InvokeLoadService(datas, config);
                    }
                );
        }

        public IObservable<Unit> LoadObservable()
        {
            return GetDataTupleObservable()
                .Select(
                    tuple =>
                    {
                        var (datas, config) = tuple;
                        InvokeLoadService(datas, config);

                        return Unit.Default;
                    }
                );
        }

        private IObservable<(ImmutableArray<TData> datas, MapConfig config)> GetDataTupleObservable()
        {
            var mapConfigurationObservable = currentMapConfigRepositoryProvider.Provide().GetMostRecent();
            var levelDataRepository = currentLevelDataRepositoryProvider.Provide();

            return GetDatasFromRepository(levelDataRepository)
                .Zip(mapConfigurationObservable, (datas, config) => (datas, config));
        }

        private void InvokeLoadService(ImmutableArray<TData> datas, MapConfig config)
        {
            var service = GetService();

            service.Load(
                datas,
                config.GetMap2DActualWidth(),
                config.GetMap2DActualHeight()
            );
        }

        private void OnDisable()
        {
            DisposeDisposables();
        }

        private void DisposeDisposables()
        {
            _disposable?.Dispose();
        }

        protected abstract ILoadBoardItemsService<TData> GetService();
        protected abstract IObservable<ImmutableArray<TData>> GetDatasFromRepository(ICurrentLevelDataRepository currentLevelDataRepository);
    }
}