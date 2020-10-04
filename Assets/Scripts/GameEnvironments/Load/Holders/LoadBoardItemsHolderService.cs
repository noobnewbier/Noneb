using System;
using Common.BoardItems;
using Common.Factories;
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
        IObservable<Unit> FinishedLoadingEventStream { get; }

        IObservable<Unit> Load(Transform mapTransform,
                               MapConfig mapConfig);
    }

    public class LoadBoardItemsHolderService<THolder, TBoardItem> : ILoadBoardItemsHolderService
        where TBoardItem : BoardItem
        where THolder : Component, IBoardItemHolder<TBoardItem>
    {
        private readonly ITilesPositionService _tilesPositionService;
        private readonly IBoardItemsGetRepository<TBoardItem> _boardItemsRepository;
        private readonly IGameObjectAndComponentFactory<THolder> _holderFactory;
        private readonly ICoordinateService _coordinateService;
        private readonly ReplaySubject<Unit> _finishedLoadingEventStream;

        public LoadBoardItemsHolderService(ITilesPositionService tilesPositionService,
                                           IBoardItemsGetRepository<TBoardItem> boardItemsRepository,
                                           IGameObjectAndComponentFactory<THolder> holderFactory,
                                           ICoordinateService coordinateService)
        {
            _tilesPositionService = tilesPositionService;
            _boardItemsRepository = boardItemsRepository;
            _holderFactory = holderFactory;
            _coordinateService = coordinateService;
            _finishedLoadingEventStream = new ReplaySubject<Unit>(1);
        }

        public IObservable<Unit> FinishedLoadingEventStream => _finishedLoadingEventStream;


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
                            var (component, gameObject) = _holderFactory.Create(mapTransform, false);
                            var flattenedIndex = _coordinateService.GetFlattenArrayIndexFromAxialCoordinate(
                                item.Coordinate.X,
                                item.Coordinate.Z,
                                mapConfig
                            );

                            gameObject.transform.position = positions[flattenedIndex];
                            gameObject.transform.parent = mapTransform;

                            component.Initialize(item);
                        }

                        _finishedLoadingEventStream.OnNext(Unit.Default);
                        return Unit.Default;
                    }
                );
        }

        public void Dispose()
        {
            _finishedLoadingEventStream?.Dispose();
        }
    }
}