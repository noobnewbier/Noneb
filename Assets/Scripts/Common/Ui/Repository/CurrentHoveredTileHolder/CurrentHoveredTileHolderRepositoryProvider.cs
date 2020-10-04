using System;
using Common.Providers;
using UnityEngine;
using UnityUtils.Constants;

namespace Common.Ui.Repository.CurrentHoveredTileHolder
{
    [CreateAssetMenu(
        fileName = nameof(CurrentHoveredTileHolderRepositoryProvider),
        menuName = MenuName.ScriptableRepository + nameof(CurrentHoveredTileHolderRepository)
    )]
    public class CurrentHoveredTileHolderRepositoryProvider : ScriptableObjectProvider<CurrentHoveredTileHolderRepository>
    {
        private readonly Lazy<CurrentHoveredTileHolderRepository> _lazyInstance =
            new Lazy<CurrentHoveredTileHolderRepository>(() => new CurrentHoveredTileHolderRepository());

        public override CurrentHoveredTileHolderRepository Provide() => _lazyInstance.Value;
    }
}