using System;
using Main.Core.Game.Common.Providers;
using UnityEngine;
using UnityUtils.Constants;

namespace Main.Ui.Game.UiState.CurrentMapTransform
{
    [CreateAssetMenu(
        fileName = nameof(CurrentMapTransformRepositoryProvider),
        menuName = MenuName.ScriptableRepository + nameof(CurrentMapTransformRepository)
    )]
    public class CurrentMapTransformRepositoryProvider : ScriptableObject, IObjectProvider<CurrentMapTransformRepository>
    {
        private readonly Lazy<CurrentMapTransformRepository> _lazyInstance =
            new Lazy<CurrentMapTransformRepository>(() => new CurrentMapTransformRepository());

        public CurrentMapTransformRepository Provide() => _lazyInstance.Value;
    }
}