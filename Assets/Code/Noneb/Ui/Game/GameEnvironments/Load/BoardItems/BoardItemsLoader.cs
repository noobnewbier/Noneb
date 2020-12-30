using System;
using System.Collections.Immutable;
using Noneb.Core.Game.Common.BoardItems;
using Noneb.Core.Game.Common.Loaders;
using Noneb.Core.Game.GameEnvironments.Load;
using Noneb.Core.Game.GameState.LevelDatas;
using Noneb.Core.Game.GameState.MapConfigs;
using Noneb.Core.Game.Maps;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;

namespace Noneb.Ui.Game.GameEnvironments.Load.BoardItems
{
    public abstract class BoardItemsLoader<TData> : ScriptableObject, ILoader where TData : BoardItemData
    {
        [FormerlySerializedAs("currentLevelDataRepositoryProvider")] [SerializeField]
        private LevelDataRepositoryProvider selectedLevelDataRepositoryProvider;

        [FormerlySerializedAs("currentMapConfigRepositoryProvider")] [SerializeField]
        private MapConfigRepositoryProvider selectedMapConfigRepositoryProvider;

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
            var mapConfigurationObservable = selectedMapConfigRepositoryProvider.Provide().GetMostRecent();
            var levelDataRepository = selectedLevelDataRepositoryProvider.Provide();

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
        protected abstract IObservable<ImmutableArray<TData>> GetDatasFromRepository(ILevelDataRepository levelDataRepository);
    }
}