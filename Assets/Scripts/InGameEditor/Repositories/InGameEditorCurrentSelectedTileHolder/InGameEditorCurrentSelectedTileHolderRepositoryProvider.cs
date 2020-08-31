using System;
using Common.Providers;
using UnityEngine;
using UnityUtils.Constants;

namespace InGameEditor.Repositories.InGameEditorCurrentSelectedTileHolder
{
    [CreateAssetMenu(
        fileName = nameof(InGameEditorCurrentSelectedTileHolderRepositoryProvider),
        menuName = MenuName.ScriptableRepository + nameof(InGameEditorCurrentSelectedTileHolderRepository)
    )]
    public class InGameEditorCurrentSelectedTileHolderRepositoryProvider : ScriptableObjectProvider<InGameEditorCurrentSelectedTileHolderRepository>
    {
        private readonly Lazy<InGameEditorCurrentSelectedTileHolderRepository> _lazyInstance =
            new Lazy<InGameEditorCurrentSelectedTileHolderRepository>(() => new InGameEditorCurrentSelectedTileHolderRepository());

        public override InGameEditorCurrentSelectedTileHolderRepository Provide()
        {
            return _lazyInstance.Value;
        }
    }
}