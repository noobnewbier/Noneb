using Common.Providers;
using GameEnvironments.Common.Repositories.CurrentGameEnvironment;
using UnityEngine;
using UnityUtils.Constants;

namespace WorldConfigurations
{
    [CreateAssetMenu(
        fileName = nameof(WorldConfigurationProvider),
        menuName = MenuName.Providers + nameof(WorldConfiguration)
    )]
    public class WorldConfigurationProvider : ScriptableObjectProvider<WorldConfiguration>
    {
        [SerializeField] private CurrentGameEnvironmentRepositoryProvider gameEnvironmentRepositoryProvider;

        private ICurrentGameEnvironmentRepository _repositoryCache;

        public override WorldConfiguration Provide()
        {
            if (_repositoryCache == null)
            {
                _repositoryCache = gameEnvironmentRepositoryProvider.Provide();
            }

            return _repositoryCache.Get().WorldConfiguration;
        }
    }
}