using System;
using Common.Providers;
using Units.Holders;
using UnityEngine;
using UnityUtils.Constants;

namespace GameEnvironments.Common.Repositories.BoardItemsHolder.Providers
{
    [CreateAssetMenu(fileName = nameof(UnitsHolderRepositoryProvider), menuName = MenuName.Providers + "UnitsHolderRepository")]
    public class UnitsHolderRepositoryProvider : ScriptableObjectProvider<BoardItemsHolderRepository<UnitHolder>>
    {
        private readonly Lazy<BoardItemsHolderRepository<UnitHolder>> _lazyInstance =
            new Lazy<BoardItemsHolderRepository<UnitHolder>>(() => new BoardItemsHolderRepository<UnitHolder>());

        public override BoardItemsHolderRepository<UnitHolder> Provide()
        {
            return _lazyInstance.Value;
        }
    }
}