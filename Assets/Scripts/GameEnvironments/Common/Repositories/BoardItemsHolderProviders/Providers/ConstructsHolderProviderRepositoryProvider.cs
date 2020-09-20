using System;
using Common.BoardItems;
using Common.Providers;
using Constructs;
using UnityEngine;
using UnityUtils.Constants;

namespace GameEnvironments.Common.Repositories.BoardItemsHolderProviders.Providers
{
    [CreateAssetMenu(
        fileName = nameof(ConstructsHolderProviderRepositoryProvider),
        menuName = MenuName.ScriptableRepository + "ConstructsHolderProviderRepository"
    )]
    public class ConstructsHolderProviderRepositoryProvider : ScriptableObjectProvider<
        BoardItemsHolderProviderRepository<BoardItemsHolderProvider<ConstructHolder>, ConstructHolder>>
    {
        private readonly Lazy<BoardItemsHolderProviderRepository<BoardItemsHolderProvider<ConstructHolder>, ConstructHolder>> _lazyInstance =
            new Lazy<BoardItemsHolderProviderRepository<BoardItemsHolderProvider<ConstructHolder>, ConstructHolder>>(
                () => new BoardItemsHolderProviderRepository<BoardItemsHolderProvider<ConstructHolder>, ConstructHolder>()
            );

        public override BoardItemsHolderProviderRepository<BoardItemsHolderProvider<ConstructHolder>, ConstructHolder> Provide()
        {
            return _lazyInstance.Value;
        }
    }
}