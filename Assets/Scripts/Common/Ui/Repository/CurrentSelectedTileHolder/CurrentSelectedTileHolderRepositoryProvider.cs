using System;
using Common.Providers;
using UnityEngine;
using UnityUtils.Constants;

namespace Common.Ui.Repository.CurrentSelectedTileHolder
{
    [CreateAssetMenu(
        fileName = nameof(CurrentSelectedTileHolderRepositoryProvider),
        menuName = MenuName.ScriptableRepository + nameof(CurrentSelectedTileHolderRepository)
    )]
    public class CurrentSelectedTileHolderRepositoryProvider : ScriptableObjectProvider<CurrentSelectedTileHolderRepository>
    {
        private readonly Lazy<CurrentSelectedTileHolderRepository> _lazyInstance =
            new Lazy<CurrentSelectedTileHolderRepository>(() => new CurrentSelectedTileHolderRepository());

        public override CurrentSelectedTileHolderRepository Provide()
        {
            return _lazyInstance.Value;
        }
    }
}