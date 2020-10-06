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
    public class CurrentMapConfigRepositoryProvider : ScriptableObjectProvider<ICurrentMapConfigRepository>
    {
        [SerializeField] private CurrentGameEnvironmentRepositoryProvider gameEnvironmentRepositoryProvider;

        private ICurrentMapConfigRepository _cache;

        public override ICurrentMapConfigRepository Provide() =>
            _cache ?? (_cache = new CurrentMapConfigRepository(gameEnvironmentRepositoryProvider.Provide()));
    }
}