using System;
using System.Collections.Generic;
using Common;
using Common.BoardItems;
using Common.Holders;
using UniRx;

namespace GameEnvironments.Common.Repositories.BoardItemsHolders
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
                    .Select(fetcher => fetcher.Provide())
                    .First();
        }
    }
}