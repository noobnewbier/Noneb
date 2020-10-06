using System;
using Main.Core.Game.Common.Providers;
using UnityEngine;
using UnityUtils.Constants;

namespace Main.Ui.Game.Common.UiState.CurrentHoveredTileHolder
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