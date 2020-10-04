using System;
using Common.BoardItems;
using Common.Providers;
using Constructs;
using UnityEngine;
using UnityUtils.Constants;

namespace GameEnvironments.Common.Repositories.BoardItemsHolderProviders.Providers
{
    [CreateAssetMenu(
        fileName = nameof(ConstructsHolderFetcherRepositoryProvider),
        menuName = MenuName.ScriptableRepository + "ConstructsHolderFetcherRepository"
    )]
    public class ConstructsHolderFetcherRepositoryProvider : ScriptableObjectProvider<
        BoardItemsHolderFetcherRepository<BoardItemsHolderFetcher<ConstructHolder>, ConstructHolder>>
    {
        private readonly Lazy<BoardItemsHolderFetcherRepository<BoardItemsHolderFetcher<ConstructHolder>, ConstructHolder>> _lazyInstance =
            new Lazy<BoardItemsHolderFetcherRepository<BoardItemsHolderFetcher<ConstructHolder>, ConstructHolder>>(
                () => new BoardItemsHolderFetcherRepository<BoardItemsHolderFetcher<ConstructHolder>, ConstructHolder>()
            );

        public override BoardItemsHolderFetcherRepository<BoardItemsHolderFetcher<ConstructHolder>, ConstructHolder> Provide() => _lazyInstance.Value;
    }
}