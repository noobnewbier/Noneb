using System;
using Common.BoardItems;
using Common.Holders;
using Common.Providers;
using GameEnvironments.Common.Repositories.BoardItems;
using Maps;
using Maps.Services;
using UniRx;
using UnityEngine;

namespace GameEnvironments.Load.Holders
{
    public interface ILoadBoardItemsHolderService : IDisposable
    {
        Subject<Unit> FinishedLoadingEventStream { get; }

        IObservable<Unit> Load(Transform mapTransform,
                               MapConfig mapConfig);
    }

    public class LoadBoardItemsHolderService<THolder, TBoardItem> : ILoadBoardItemsHolderService
        where TBoardItem : BoardItem
        where THolder : Component, IBoardItemHolder<TBoardItem>
    {
        private readonly ITilesPositionService _tilesPositionService;
        private readonly IBoardItemsGetRepository<TBoardItem> _boardItemsRepository;
        private readonly IGameObjectAndComponentProvider<THolder> _holderProvider;
        private readonly ICoordinateService _coordinateService;

        public LoadBoardItemsHolderService(ITilesPositionService tilesPositionService,
                                           IBoardItemsGetRepository<TBoardItem> boardItemsRepository,
                                           IGameObjectAndComponentProvider<THolder> holderProvider,
                                           ICoordinateService coordinateService)
        {
            _tilesPositionService = tilesPositionService;
            _boardItemsRepository = boardItemsRepository;
            _holderProvider = holderProvider;
            _coordinateService = coordinateService;
            FinishedLoadingEventStream = new Subject<Unit>();
        }

        public Subject<Unit> FinishedLoadingEventStream { get; }


        public IObservable<Unit> Load(Transform mapTransform, MapConfig mapConfig)
        {
            return _tilesPositionService
                .GetMostRecent(mapTransform.position.y)
                .Zip(_boardItemsRepository.GetMostRecent(), (positions, items) => (positions, items))
                .Select(
                    tuple =>
                    {
                        var (positions, items) = tuple;

                        foreach (var item in items)
                        {
                            var (component, gameObject) = _holderProvider.Provide(mapTransform, false);
                            var flattenedIndex = _coordinateService.GetFlattenArrayIndexFromAxialCoordinate(item.Coordinate.X, item.Coordinate.Z, mapConfig);

                            gameObject.transform.position = positions[flattenedIndex];
                            gameObject.transform.parent = mapTransform;

                            component.Initialize(item);
                        }

                        return Unit.Default;
                    }
                );
        }

        public void Dispose()
        {
            FinishedLoadingEventStream?.Dispose();
        }
    }
}