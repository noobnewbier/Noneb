using Common.Providers;
using GameEnvironments.Common.Repositories.CurrentGameEnvironment;
using UnityEngine;
using UnityUtils.Constants;

namespace Maps.Repositories
{
    [CreateAssetMenu(fileName = nameof(MapConfigurationRepositoryProvider), menuName = MenuName.ScriptableRepository+nameof(MapConfigurationRepository))]
    public class MapConfigurationRepositoryProvider : ScriptableObjectProvider<IMapConfigurationRepository>
    {
        [SerializeField] private CurrentGameEnvironmentRepositoryProvider gameEnvironmentRepositoryProvider;

        private IMapConfigurationRepository _cache;
        
        public override IMapConfigurationRepository Provide()
        {
            return _cache ?? (_cache = new MapConfigurationRepository(gameEnvironmentRepositoryProvider.Provide()));
        }
    }
}