using System;
using Noneb.Core.Game.Common.Providers;
using Noneb.Ui.Game.Common.Holders;
using Noneb.Ui.Game.Units;
using UnityEngine;
using UnityUtils.Constants;

namespace Noneb.Ui.Game.UiState.BoardItemsFetcher.Providers
{
    [CreateAssetMenu(
        fileName = nameof(UnitsHolderFetcherRepositoryProvider),
        menuName = MenuName.ScriptableRepository + "UnitsHolderFetcherRepository"
    )]
    public class UnitsHolderFetcherRepositoryProvider : ScriptableObject,
                                                        IObjectProvider<BoardItemsHolderFetcherRepository<BoardItemsHolderFetcher<UnitHolder>, UnitHolder>>
    {
        private readonly Lazy<BoardItemsHolderFetcherRepository<BoardItemsHolderFetcher<UnitHolder>, UnitHolder>> _lazyInstance =
            new Lazy<BoardItemsHolderFetcherRepository<BoardItemsHolderFetcher<UnitHolder>, UnitHolder>>(
                () => new BoardItemsHolderFetcherRepository<BoardItemsHolderFetcher<UnitHolder>, UnitHolder>()
            );

        public BoardItemsHolderFetcherRepository<BoardItemsHolderFetcher<UnitHolder>, UnitHolder> Provide() => _lazyInstance.Value;
    }
}