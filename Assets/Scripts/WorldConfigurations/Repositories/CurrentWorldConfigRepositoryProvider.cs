using Common.Providers;
using GameEnvironments.Common.Repositories.CurrentGameEnvironments;
using UnityEngine;
using UnityUtils.Constants;

namespace WorldConfigurations.Repositories
{
    [CreateAssetMenu(
        fileName = nameof(CurrentWorldConfigRepositoryProvider),
        menuName = MenuName.ScriptableRepository + nameof(CurrentWorldConfigRepository)
    )]
    public class CurrentWorldConfigRepositoryProvider : ScriptableObjectProvider<ICurrentWorldConfigRepository>
    {
        [SerializeField] private CurrentGameEnvironmentRepositoryProvider currentGameEnvironmentRepositoryProvider;

        private ICurrentWorldConfigRepository _cache;

        public override ICurrentWorldConfigRepository Provide()
        {
            return _cache ?? (_cache = new CurrentWorldConfigRepository(currentGameEnvironmentRepositoryProvider.Provide()));
        }
    }
}