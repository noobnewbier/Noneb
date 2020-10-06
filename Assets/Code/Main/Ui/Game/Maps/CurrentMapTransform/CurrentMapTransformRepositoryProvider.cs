using System;
using Main.Core.Game.Common.Providers;
using UnityEngine;
using UnityUtils.Constants;

namespace Main.Ui.Game.Maps.CurrentMapTransform
{
    [CreateAssetMenu(
        fileName = nameof(CurrentMapTransformRepositoryProvider),
        menuName = MenuName.ScriptableRepository + nameof(CurrentMapTransformRepository)
    )]
    public class CurrentMapTransformRepositoryProvider : ScriptableObjectProvider<CurrentMapTransformRepository>
    {
        private readonly Lazy<CurrentMapTransformRepository> _lazyInstance =
            new Lazy<CurrentMapTransformRepository>(() => new CurrentMapTransformRepository());

        public override CurrentMapTransformRepository Provide() => _lazyInstance.Value;
    }
}