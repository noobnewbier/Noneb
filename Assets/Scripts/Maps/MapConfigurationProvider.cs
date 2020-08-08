using Common.Providers;
using GameEnvironments.Common.Repositories.CurrentGameEnvironment;
using UnityEngine;
using UnityUtils.Constants;

namespace Maps
{
    [CreateAssetMenu(fileName = nameof(MapConfigurationProvider), menuName = MenuName.Providers + nameof(MapConfigurationProvider))]
    public class MapConfigurationProvider : ScriptableObjectProvider<MapConfiguration>
    {
        [SerializeField] private CurrentGameEnvironmentRepositoryProvider gameEnvironmentRepositoryProvider;

        private ICurrentGameEnvironmentRepository _repositoryCache;

        public override MapConfiguration Provide()
        {
            if (_repositoryCache == null)
            {
                _repositoryCache = gameEnvironmentRepositoryProvider.Provide();
            }

            return _repositoryCache.Get().MapConfiguration;
        }
    }
}