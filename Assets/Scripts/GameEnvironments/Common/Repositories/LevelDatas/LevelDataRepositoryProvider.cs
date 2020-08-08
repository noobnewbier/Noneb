using Common.Providers;
using GameEnvironments.Common.Repositories.CurrentGameEnvironment;
using UnityEngine;
using UnityUtils.Constants;

namespace GameEnvironments.Common.Repositories.LevelDatas
{
    [CreateAssetMenu(fileName = nameof(LevelDataRepositoryProvider), menuName = MenuName.ScriptableRepository + nameof(LevelDataRepository))]
    public class LevelDataRepositoryProvider : ScriptableObjectProvider<ILevelDataRepository>
    {
        [SerializeField] private CurrentGameEnvironmentRepositoryProvider currentGameEnvironmentRepositoryProvider;

        private ILevelDataRepository _cache;

        public override ILevelDataRepository Provide()
        {
            return _cache ??
                   (_cache = new LevelDataRepository(
                       currentGameEnvironmentRepositoryProvider.Provide().Get().LevelData
                   ));
        }
    }
}