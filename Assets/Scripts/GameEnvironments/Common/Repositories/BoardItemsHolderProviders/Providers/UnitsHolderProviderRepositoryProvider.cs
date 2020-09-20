using System;
using Common.BoardItems;
using Common.Providers;
using Units.Holders;
using UnityEngine;
using UnityUtils.Constants;

namespace GameEnvironments.Common.Repositories.BoardItemsHolderProviders.Providers
{
    [CreateAssetMenu(
        fileName = nameof(UnitsHolderProviderRepositoryProvider),
        menuName = MenuName.ScriptableRepository + "UnitsHolderProviderRepository"
    )]
    public class UnitsHolderProviderRepositoryProvider : ScriptableObjectProvider<
        BoardItemsHolderProviderRepository<BoardItemsHolderProvider<UnitHolder>, UnitHolder>>
    {
        private readonly Lazy<BoardItemsHolderProviderRepository<BoardItemsHolderProvider<UnitHolder>, UnitHolder>> _lazyInstance =
            new Lazy<BoardItemsHolderProviderRepository<BoardItemsHolderProvider<UnitHolder>, UnitHolder>>(
                () => new BoardItemsHolderProviderRepository<BoardItemsHolderProvider<UnitHolder>, UnitHolder>()
            );

        public override BoardItemsHolderProviderRepository<BoardItemsHolderProvider<UnitHolder>, UnitHolder> Provide()
        {
            return _lazyInstance.Value;
        }
    }
}