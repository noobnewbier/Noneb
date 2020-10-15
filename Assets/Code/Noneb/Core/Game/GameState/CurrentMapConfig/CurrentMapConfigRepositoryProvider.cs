using Noneb.Core.Game.Common.Providers;
using Noneb.Core.Game.GameState.CurrentGameEnvironments;
using UnityEngine;
using UnityUtils.Constants;

namespace Noneb.Core.Game.GameState.CurrentMapConfig
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