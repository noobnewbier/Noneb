using Common.Providers;
using GameEnvironments.Common.Repositories.CurrentGameEnvironment;
using UnityEngine;
using UnityUtils.Constants;

namespace WorldConfigurations.Repositories
{
    [CreateAssetMenu(fileName = nameof(WorldConfigurationRepositoryProvider), menuName = MenuName.ScriptableRepository + nameof(WorldConfigurationRepository))]
    public class WorldConfigurationRepositoryProvider : ScriptableObjectProvider<IWorldConfigurationRepository>
    {
        [SerializeField] private CurrentGameEnvironmentRepositoryProvider currentGameEnvironmentRepositoryProvider;

        private IWorldConfigurationRepository _cache;
        
        public override IWorldConfigurationRepository Provide()
        {
            return _cache ?? (_cache = new WorldConfigurationRepository(currentGameEnvironmentRepositoryProvider.Provide()));
        }
    }
}