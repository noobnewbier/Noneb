using System;
using Noneb.Core.Game.Common.Providers;
using UnityEngine;
using UnityUtils.Constants;

namespace Noneb.Core.Game.GameState.GameEnvironments
{
    [CreateAssetMenu(
        fileName = nameof(SelectedGameEnvironmentRepositoryProvider),
        menuName = MenuName.ScriptableRepository + nameof(GameEnvironmentRepository)
    )]
    public class SelectedGameEnvironmentRepositoryProvider : ScriptableObject, IObjectProvider<IGameEnvironmentRepository>
    {
        private readonly Lazy<IGameEnvironmentRepository> _lazyInstance =
            new Lazy<IGameEnvironmentRepository>(() => new GameEnvironmentRepository());

        public IGameEnvironmentRepository Provide() => _lazyInstance.Value;
    }
}