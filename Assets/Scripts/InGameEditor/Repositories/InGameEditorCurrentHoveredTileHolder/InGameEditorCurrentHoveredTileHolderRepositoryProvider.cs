using System;
using Common.Providers;
using UnityEngine;
using UnityUtils.Constants;

namespace InGameEditor.Repositories.InGameEditorCurrentHoveredTileHolder
{
    [CreateAssetMenu(
        fileName = nameof(InGameEditorCurrentHoveredTileHolderRepositoryProvider),
        menuName = MenuName.ScriptableRepository + nameof(InGameEditorCurrentHoveredTileHolderRepository)
    )]
    public class InGameEditorCurrentHoveredTileHolderRepositoryProvider : ScriptableObjectProvider<InGameEditorCurrentHoveredTileHolderRepository>
    {
        private readonly Lazy<InGameEditorCurrentHoveredTileHolderRepository> _lazyInstance =
            new Lazy<InGameEditorCurrentHoveredTileHolderRepository>(() => new InGameEditorCurrentHoveredTileHolderRepository());

        public override InGameEditorCurrentHoveredTileHolderRepository Provide()
        {
            return _lazyInstance.Value;
        }
    }
}