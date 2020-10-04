using System;
using Common.Providers;
using UnityEngine;
using UnityUtils.Constants;

namespace GameEnvironments.Common.Repositories.CurrentGameEnvironments
{
    [CreateAssetMenu(
        fileName = nameof(CurrentGameEnvironmentRepositoryProvider),
        menuName = MenuName.ScriptableRepository + nameof(CurrentGameEnvironmentRepository)
    )]
    public class CurrentGameEnvironmentRepositoryProvider : ScriptableObjectProvider<CurrentGameEnvironmentRepository>
    {
        private readonly Lazy<CurrentGameEnvironmentRepository> _lazyInstance =
            new Lazy<CurrentGameEnvironmentRepository>(() => new CurrentGameEnvironmentRepository());

        public override CurrentGameEnvironmentRepository Provide() => _lazyInstance.Value;
    }
}