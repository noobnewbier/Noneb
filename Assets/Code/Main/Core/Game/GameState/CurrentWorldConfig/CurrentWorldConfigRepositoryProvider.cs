using Main.Core.Game.Common.Providers;
using Main.Core.Game.GameState.CurrentGameEnvironments;
using UnityEngine;
using UnityUtils.Constants;

namespace Main.Core.Game.GameState.CurrentWorldConfig
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