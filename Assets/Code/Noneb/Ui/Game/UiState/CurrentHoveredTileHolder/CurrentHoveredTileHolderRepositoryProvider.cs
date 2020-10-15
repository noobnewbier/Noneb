using System;
using Noneb.Core.Game.Common.Providers;
using UnityEngine;
using UnityUtils.Constants;

namespace Noneb.Ui.Game.UiState.CurrentHoveredTileHolder
{
    [CreateAssetMenu(
        fileName = nameof(CurrentHoveredTileHolderRepositoryProvider),
        menuName = MenuName.ScriptableRepository + nameof(CurrentHoveredTileHolderRepository)
    )]
    public class CurrentHoveredTileHolderRepositoryProvider : ScriptableObject, IObjectProvider<CurrentHoveredTileHolderRepository>
    {
        private readonly Lazy<CurrentHoveredTileHolderRepository> _lazyInstance =
            new Lazy<CurrentHoveredTileHolderRepository>(() => new CurrentHoveredTileHolderRepository());

        public CurrentHoveredTileHolderRepository Provide() => _lazyInstance.Value;
    }
}