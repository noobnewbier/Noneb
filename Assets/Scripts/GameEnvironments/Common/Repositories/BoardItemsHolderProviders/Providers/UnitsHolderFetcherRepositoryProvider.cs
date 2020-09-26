using System;
using Common.BoardItems;
using Common.Providers;
using Units.Holders;
using UnityEngine;
using UnityUtils.Constants;

namespace GameEnvironments.Common.Repositories.BoardItemsHolderProviders.Providers
{
    [CreateAssetMenu(
        fileName = nameof(UnitsHolderFetcherRepositoryProvider),
        menuName = MenuName.ScriptableRepository + "UnitsHolderFetcherRepository"
    )]
    public class UnitsHolderFetcherRepositoryProvider : ScriptableObjectProvider<
        BoardItemsHolderFetcherRepository<BoardItemsHolderFetcher<UnitHolder>, UnitHolder>>
    {
        private readonly Lazy<BoardItemsHolderFetcherRepository<BoardItemsHolderFetcher<UnitHolder>, UnitHolder>> _lazyInstance =
            new Lazy<BoardItemsHolderFetcherRepository<BoardItemsHolderFetcher<UnitHolder>, UnitHolder>>(
                () => new BoardItemsHolderFetcherRepository<BoardItemsHolderFetcher<UnitHolder>, UnitHolder>()
            );

        public override BoardItemsHolderFetcherRepository<BoardItemsHolderFetcher<UnitHolder>, UnitHolder> Provide()
        {
            return _lazyInstance.Value;
        }
    }
}