using Common.Providers;
using GameEnvironments.Common.Repositories.CurrentGameEnvironments;
using UnityEngine;
using UnityUtils.Constants;

namespace GameEnvironments.Common.Repositories.CurrentLevelDatas
{
    [CreateAssetMenu(
        fileName = nameof(CurrentLevelDataRepositoryProvider),
        menuName = MenuName.ScriptableRepository + nameof(CurrentLevelDataRepository)
    )]
    public class CurrentLevelDataRepositoryProvider : ScriptableObjectProvider<ICurrentLevelDataRepository>
    {
        [SerializeField] private CurrentGameEnvironmentRepositoryProvider currentGameEnvironmentRepositoryProvider;

        private ICurrentLevelDataRepository _cache;

        public override ICurrentLevelDataRepository Provide()
        {
            return _cache ??
                   (_cache = new CurrentLevelDataRepository(
                       currentGameEnvironmentRepositoryProvider.Provide()
                   ));
        }
    }
}