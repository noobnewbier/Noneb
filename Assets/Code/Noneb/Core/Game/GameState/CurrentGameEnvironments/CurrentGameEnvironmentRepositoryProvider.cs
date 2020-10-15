using System;
using Noneb.Core.Game.Common.Providers;
using UnityEngine;
using UnityUtils.Constants;

namespace Noneb.Core.Game.GameState.CurrentGameEnvironments
{
    [CreateAssetMenu(
        fileName = nameof(CurrentGameEnvironmentRepositoryProvider),
        menuName = MenuName.ScriptableRepository + nameof(CurrentGameEnvironmentRepository)
    )]
    public class CurrentGameEnvironmentRepositoryProvider : ScriptableObject, IObjectProvider<ICurrentGameEnvironmentRepository>
    {
        private readonly Lazy<ICurrentGameEnvironmentRepository> _lazyInstance =
            new Lazy<ICurrentGameEnvironmentRepository>(() => new CurrentGameEnvironmentRepository());

        public ICurrentGameEnvironmentRepository Provide() => _lazyInstance.Value;
    }
}