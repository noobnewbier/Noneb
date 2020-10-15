using Noneb.Core.Game.Common.Providers;
using Noneb.Core.Game.GameState.CurrentGameEnvironments;
using UnityEngine;
using UnityUtils.Constants;

namespace Noneb.Core.Game.GameState.CurrentWorldConfig
{
    [CreateAssetMenu(
        fileName = nameof(CurrentWorldConfigRepositoryProvider),
        menuName = MenuName.ScriptableRepository + nameof(CurrentWorldConfigRepository)
    )]
    public class CurrentWorldConfigRepositoryProvider : ScriptableObject, IObjectProvider<ICurrentWorldConfigRepository>
    {
        [SerializeField] private CurrentGameEnvironmentRepositoryProvider currentGameEnvironmentRepositoryProvider;

        private ICurrentWorldConfigRepository _cache;

        public ICurrentWorldConfigRepository Provide() =>
            _cache ?? (_cache = new CurrentWorldConfigRepository(currentGameEnvironmentRepositoryProvider.Provide()));
    }
}