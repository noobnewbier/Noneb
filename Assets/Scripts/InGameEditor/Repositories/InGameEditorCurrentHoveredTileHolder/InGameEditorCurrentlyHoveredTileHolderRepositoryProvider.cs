using System;
using Common.Providers;
using UnityEngine;
using UnityUtils.Constants;

namespace InGameEditor.Repositories.InGameEditorCurrentHoveredTileHolder
{
    [CreateAssetMenu(
        fileName = nameof(InGameEditorCurrentlyHoveredTileHolderRepositoryProvider),
        menuName = MenuName.ScriptableRepository + nameof(InGameEditorCurrentlyHoveredTileHolderRepository)
    )]
    public class InGameEditorCurrentlyHoveredTileHolderRepositoryProvider : ScriptableObjectProvider<InGameEditorCurrentlyHoveredTileHolderRepository>
    {
        private readonly Lazy<InGameEditorCurrentlyHoveredTileHolderRepository> _lazyInstance =
            new Lazy<InGameEditorCurrentlyHoveredTileHolderRepository>(() => new InGameEditorCurrentlyHoveredTileHolderRepository());

        public override InGameEditorCurrentlyHoveredTileHolderRepository Provide()
        {
            return _lazyInstance.Value;
        }
    }
}