using Main.Core.Game.Common.Providers;
using Main.Core.Game.GameEnvironments.CurrentGameEnvironments;
using UnityEngine;
using UnityUtils.Constants;

namespace Main.Core.Game.Maps.CurrentMapConfig
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