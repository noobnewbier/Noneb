using System;
using Common.Providers;
using Units;
using Units.Holders;
using UnityEngine;
using UnityUtils.Constants;

namespace GameEnvironments.Common.Repositories.BoardItemsHolderProvider.Providers
{
    [CreateAssetMenu(
        fileName = nameof(UnitsHolderProviderRepositoryProvider),
        menuName = MenuName.ScriptableRepository + "UnitsHolderProviderRepository"
    )]
    public class UnitsHolderProviderRepositoryProvider : ScriptableObjectProvider<BoardItemsHolderProviderRepository<UnitsHolderProvider, UnitHolder>>
    {
        private readonly Lazy<BoardItemsHolderProviderRepository<UnitsHolderProvider, UnitHolder>> _lazyInstance =
            new Lazy<BoardItemsHolderProviderRepository<UnitsHolderProvider, UnitHolder>>(
                () => new BoardItemsHolderProviderRepository<UnitsHolderProvider, UnitHolder>()
            );

        public override BoardItemsHolderProviderRepository<UnitsHolderProvider, UnitHolder> Provide()
        {
            return _lazyInstance.Value;
        }
    }
}