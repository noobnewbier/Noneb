using System;
using Main.Core.Game.Common.BoardItems;
using Main.Core.Game.Common.Factories;
using Main.Core.Game.Coordinates;
using Main.Core.Game.GameState.BoardItems;
using Main.Core.Game.Maps;
using Main.Ui.Game.Common.Holders;
using Main.Ui.Game.Maps.TilesPosition;
using UniRx;
using UnityEngine;

namespace Main.Ui.Game.GameEnvironments.Load.Holders
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