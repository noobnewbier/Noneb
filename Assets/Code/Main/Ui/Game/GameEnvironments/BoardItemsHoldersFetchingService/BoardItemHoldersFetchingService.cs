using System;
using System.Collections.Generic;
using Main.Core.Game.Common;
using Main.Ui.Game.Common.Holders;
using UniRx;

namespace Main.Ui.Game.GameEnvironments.BoardItemsHoldersFetchingService
{
    public interface IBoardItemHoldersFetchingService<out THolder> where THolder : IBoardItemHolder
    {
        IObservable<IReadOnlyList<THolder>> Fetch();
    }


    public class BoardItemHoldersFetchingService<THolder> : IBoardItemHoldersFetchingService<THolder>
        where THolder : IBoardItemHolder
    {
        private readonly IDataGetRepository<BoardItemsHolderFetcher<THolder>> _holdersFetcherRepository;

        public BoardItemHoldersFetchingService(IDataGetRepository<BoardItemsHolderFetcher<THolder>> holdersFetcherRepository)
        {
            _holdersFetcherRepository = holdersFetcherRepository;
        }

        public IObservable<IReadOnlyList<THolder>> Fetch()
        {
            return _holdersFetcherRepository.GetMostRecent()
                .Select(fetcher => fetcher.Fetch())
                .First();
        }
    }
}