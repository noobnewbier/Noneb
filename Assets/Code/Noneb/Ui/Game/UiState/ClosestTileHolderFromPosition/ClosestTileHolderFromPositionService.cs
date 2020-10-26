using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Noneb.Core.Game.GameState.WorldConfig;
using Noneb.Core.Game.WorldConfigurations;
using Noneb.Ui.Game.GameEnvironments.BoardItemsHoldersFetchingService;
using Noneb.Ui.Game.GameEnvironments.Load.Holders;
using Noneb.Ui.Game.Tiles;
using UniRx;
using UnityEngine;
using UnityUtils;

namespace Noneb.Ui.Game.UiState.ClosestTileHolderFromPosition
{
    public interface IClosestTileHolderFromPositionService : IDisposable
    {
        TileHolder GetTileHolderFromPosition(Vector3 position);
    }

    public class ClosestTileHolderFromPositionService : IClosestTileHolderFromPositionService
    {
        private readonly IDisposable _compositeDisposable;
        private readonly IBoardItemHoldersFetchingService<TileHolder> _holderFetchingService;

        private IReadOnlyList<TileHolder> _currentTileHolders;
        private WorldConfig _currentWorldConfig;
        private IDisposable _fetchingServiceDisposable;
        private TileHolder _previousClickedTileHolder;

        public ClosestTileHolderFromPositionService(IWorldConfigRepository loadedWorldConfigRepository,
                                                    IBoardItemHoldersFetchingService<TileHolder> holderFetchingService,
                                                    ILoadBoardItemsHolderService tileHolderLoadService)
        {
            _holderFetchingService = holderFetchingService;
            _currentTileHolders = new List<TileHolder>();

            _compositeDisposable = new CompositeDisposable
            {
                loadedWorldConfigRepository.GetObservableStream()
                    .SubscribeOn(Scheduler.ThreadPool)
                    .ObserveOn(Scheduler.MainThread)
                    .Subscribe(config => _currentWorldConfig = config),
                tileHolderLoadService.FinishedLoadingEventStream
                    .SubscribeOn(Scheduler.ThreadPool)
                    .ObserveOn(Scheduler.MainThread)
                    .Subscribe(_ => UpdateCurrentTileHolders())
            };
        }


        [CanBeNull]
        public TileHolder GetTileHolderFromPosition(Vector3 position)
        {
            // we don't want tiles that is literally not even touched by our cursor
            var minDistanceThreshold = _currentWorldConfig.OuterRadius;

            var holdersWithinThreshold = _currentTileHolders
                .Where(IsBehaviourValid)
                .Where(h => Vector3.Distance(h.transform.position, position) < minDistanceThreshold)
                .ToArray();

            return holdersWithinThreshold.Any() ?
                holdersWithinThreshold.MinBy(h => Vector3.Distance(h.transform.position, position)) :
                null;
        }

        public void Dispose()
        {
            _compositeDisposable?.Dispose();
            _fetchingServiceDisposable?.Dispose();
        }

        private void UpdateCurrentTileHolders()
        {
            _fetchingServiceDisposable = _holderFetchingService.Fetch()
                .Subscribe(
                    holders => { _currentTileHolders = holders; },
                    () => _fetchingServiceDisposable?.Dispose()
                );
        }

        private static bool IsBehaviourValid(Behaviour tileHolder) => tileHolder != null && tileHolder.isActiveAndEnabled;
    }
}