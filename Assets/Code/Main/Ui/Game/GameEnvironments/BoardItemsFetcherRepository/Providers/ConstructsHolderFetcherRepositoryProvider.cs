using System;
using Main.Core.Game.Common.Providers;
using Main.Ui.Game.Common.Holders;
using Main.Ui.Game.Constructs;
using UnityEngine;
using UnityUtils.Constants;

namespace Main.Ui.Game.GameEnvironments.BoardItemsFetcherRepository.Providers
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