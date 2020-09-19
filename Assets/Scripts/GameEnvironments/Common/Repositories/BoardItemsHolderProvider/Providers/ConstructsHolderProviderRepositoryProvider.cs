using System;
using Common.Providers;
using Constructs;
using UnityEngine;
using UnityUtils.Constants;

namespace GameEnvironments.Common.Repositories.BoardItemsHolderProvider.Providers
{
    [CreateAssetMenu(
        fileName = nameof(ConstructsHolderProviderRepositoryProvider),
        menuName = MenuName.ScriptableRepository + "ConstructsHolderProviderRepository"
    )]
    public class ConstructsHolderProviderRepositoryProvider : ScriptableObjectProvider<
        BoardItemsHolderProviderRepository<ConstructsHolderProvider, ConstructHolder>>
    {
        private readonly Lazy<BoardItemsHolderProviderRepository<ConstructsHolderProvider, ConstructHolder>> _lazyInstance =
            new Lazy<BoardItemsHolderProviderRepository<ConstructsHolderProvider, ConstructHolder>>(
                () => new BoardItemsHolderProviderRepository<ConstructsHolderProvider, ConstructHolder>()
            );

        public override BoardItemsHolderProviderRepository<ConstructsHolderProvider, ConstructHolder> Provide()
        {
            return _lazyInstance.Value;
        }
    }
}