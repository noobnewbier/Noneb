using System;
using Common.Providers;
using UnityEngine;
using UnityUtils.Constants;

namespace Maps.Repositories.CurrentMapTransform
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