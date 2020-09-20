using System;
using System.Collections.Generic;
using Common;
using Common.BoardItems;
using Common.Holders;
using GameEnvironments.Load.Holders;
using UniRx;

namespace GameEnvironments.Common.Repositories.BoardItemsHolders
{
    public interface IBoardItemsHolderGetRepository<out THolder> : IDataGetRepository<IReadOnlyList<THolder>>, IDisposable
        where THolder : IBoardItemHolder
    {
    }


    public class BoardItemsHolderGetRepository<THolder> : IBoardItemsHolderGetRepository<THolder>
        where THolder : IBoardItemHolder
    {
        private readonly ReplaySubject<IReadOnlyList<THolder>> _stream;
        private IObservable<IReadOnlyList<THolder>> _single;
        private readonly IDisposable _disposable;

        public BoardItemsHolderGetRepository(IDataGetRepository<BoardItemsHolderProvider<THolder>> holderProviderRepository,
                                             ILoadBoardItemsHolderService loadBoardItemsHolderService)
        {
            _stream = new ReplaySubject<IReadOnlyList<THolder>>(1);
            _disposable = loadBoardItemsHolderService.FinishedLoadingEventStream.ZipLatest(
                    holderProviderRepository.GetObservableStream(),
                    (_, provider) => provider
                )
                .Subscribe(
                    provider =>
                    {
                        var boardItemHolders = provider.Provide();
                        _stream.OnNext(boardItemHolders);
                        _single = Observable.Return(boardItemHolders);
                    }
                );
        }

        public IObservable<IReadOnlyList<THolder>> GetObservableStream()
        {
            return _stream;
        }

        public IObservable<IReadOnlyList<THolder>> GetMostRecent()
        {
            return _single;
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }
    }
}