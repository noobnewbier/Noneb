using System;
using Noneb.Core.Game.Common.Providers;
using Noneb.Ui.Game.Common.Holders;
using Noneb.Ui.Game.Constructs;
using UnityEngine;
using UnityUtils.Constants;

namespace Noneb.Ui.Game.UiState.BoardItemsFetcher.Providers
{
    [CreateAssetMenu(
        fileName = nameof(ConstructsHolderFetcherRepositoryProvider),
        menuName = MenuName.ScriptableRepository + "ConstructsHolderFetcherRepository"
    )]
    public class ConstructsHolderFetcherRepositoryProvider : ScriptableObject,
                                                             IObjectProvider<BoardItemsHolderFetcherRepository<BoardItemsHolderFetcher<ConstructHolder>, ConstructHolder>>
    {
        private readonly Lazy<BoardItemsHolderFetcherRepository<BoardItemsHolderFetcher<ConstructHolder>, ConstructHolder>> _lazyInstance =
            new Lazy<BoardItemsHolderFetcherRepository<BoardItemsHolderFetcher<ConstructHolder>, ConstructHolder>>(
                () => new BoardItemsHolderFetcherRepository<BoardItemsHolderFetcher<ConstructHolder>, ConstructHolder>()
            );

        public BoardItemsHolderFetcherRepository<BoardItemsHolderFetcher<ConstructHolder>, ConstructHolder> Provide() => _lazyInstance.Value;
    }
}