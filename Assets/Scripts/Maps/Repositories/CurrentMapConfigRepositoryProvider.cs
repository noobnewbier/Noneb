using Common.Providers;
using GameEnvironments.Common.Repositories.CurrentGameEnvironment;
using UnityEngine;
using UnityUtils.Constants;

namespace Maps.Repositories
{
    [CreateAssetMenu(fileName = nameof(CurrentMapConfigRepositoryProvider), menuName = MenuName.ScriptableRepository+nameof(CurrentMapConfigRepository))]
    public class CurrentMapConfigRepositoryProvider : ScriptableObjectProvider<ICurrentMapConfigRepository>
    {
        [SerializeField] private CurrentGameEnvironmentRepositoryProvider gameEnvironmentRepositoryProvider;

        private ICurrentMapConfigRepository _cache;
        
        public override ICurrentMapConfigRepository Provide()
        {
            return _cache ?? (_cache = new CurrentMapConfigRepository(gameEnvironmentRepositoryProvider.Provide()));
        }
    }
}