using System;
using Common.BoardItems;
using Common.Holders;
using Common.Providers;
using GameEnvironments.Common.Repositories.BoardItems;
using Maps.Services;
using UniRx;
using UnityEngine;

namespace GameEnvironments.Load.Holders
{
    public interface ILoadBoardItemsHolderService : IDisposable
    {
        IObservable<Unit> Load(Transform mapTransform,
                               int mapXSize,
                               int mapZSize);
    }

    public class LoadBoardItemsHolderService<THolder, TBoardItem> : ILoadBoardItemsHolderService
        where TBoardItem : BoardItem
        where THolder : Component, IBoardItemHolder<TBoardItem>
    {
        private readonly ITilesPositionService _tilesPositionService;
        private readonly IBoardItemsRepository<TBoardItem> _boardItemsRepository;
        private readonly Subject<Unit> _finishedLoadingEventStream;
        private readonly IGameObjectAndComponentProvider<THolder> _holderProvider;

        public LoadBoardItemsHolderService(ITilesPositionService tilesPositionService,
                                           IBoardItemsRepository<TBoardItem> boardItemsRepository,
                                           IGameObjectAndComponentProvider<THolder> holderProvider)
        {
            _tilesPositionService = tilesPositionService;
            _boardItemsRepository = boardItemsRepository;
            _holderProvider = holderProvider;
            _finishedLoadingEventStream = new Subject<Unit>();
        }

        public IObservable<Unit> Load(Transform mapTransform, int mapXSize, int mapZSize)
        {
            return _tilesPositionService
                .GetMostRecent(mapTransform.position.y)
                .Zip(_boardItemsRepository.GetMostRecent(), (positions, items) => (positions, items))
                .Select(
                    tuple =>
                    {
                        var (positions, items) = tuple;

                        for (var i = 0; i < mapZSize; i++)
                        for (var j = 0; j < mapXSize; j++)
                        {
                            var (component, gameObject) = _holderProvider.Provide(mapTransform, false);
                            var index = i * mapXSize + j;

                            gameObject.transform.position = positions[index];
                            gameObject.transform.parent = mapTransform;

                            component.Initialize(items[index]);
                        }

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