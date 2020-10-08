using Main.Core.Game.Common.Providers;
using Main.Core.Game.GameState.CurrentGameEnvironments;
using UnityEngine;
using UnityUtils.Constants;

namespace Main.Core.Game.GameState.CurrentMapConfig
{
    [CreateAssetMenu(
        fileName = nameof(CurrentMapConfigRepositoryProvider),
        menuName = MenuName.ScriptableRepository + nameof(CurrentMapConfigRepository)
    )]
    public class CurrentMapConfigRepositoryProvider : ScriptableObject, IObjectProvider<ICurrentMapConfigRepository>
    {
        [SerializeField] private CurrentGameEnvironmentRepositoryProvider gameEnvironmentRepositoryProvider;

        private ICurrentMapConfigRepository _cache;

        public ICurrentMapConfigRepository Provide() =>
            _cache ?? (_cache = new CurrentMapConfigRepository(gameEnvironmentRepositoryProvider.Provide()));
    }
}