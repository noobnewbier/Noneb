using Common.Providers;
using GameEnvironments.Common.Data;
using GameEnvironments.Common.Data.GameEnvironments;
using UnityEngine;
using UnityUtils.Constants;

namespace GameEnvironments.Common.Repositories.CurrentGameEnvironment
{
    [CreateAssetMenu(
        fileName = nameof(CurrentGameEnvironmentRepositoryProvider),
        menuName = MenuName.ScriptableRepository + nameof(CurrentGameEnvironmentRepository)
    )]
    public class CurrentGameEnvironmentRepositoryProvider : ScriptableObjectProvider<ICurrentGameEnvironmentRepository>
    {
        [SerializeField] private GameEnvironmentScriptable gameEnvironmentScriptable;

        private ICurrentGameEnvironmentRepository _cache;

        public override ICurrentGameEnvironmentRepository Provide()
        {
            return _cache ?? (_cache = new CurrentGameEnvironmentRepository(gameEnvironmentScriptable.ToGameEnvironment()));
        }
    }
}