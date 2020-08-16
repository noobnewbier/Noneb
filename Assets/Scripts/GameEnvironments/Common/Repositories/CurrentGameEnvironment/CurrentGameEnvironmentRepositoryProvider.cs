using Common.Providers;
using GameEnvironments.Common.Data;
using UnityEngine;
using UnityUtils.Constants;

namespace GameEnvironments.Common.Repositories.CurrentGameEnvironment
{
    [CreateAssetMenu(
        fileName = nameof(CurrentGameEnvironmentRepositoryProvider),
        menuName = MenuName.ScriptableRepository + nameof(CurrentGameEnvironmentRepository)
    )]
    public class CurrentGameEnvironmentRepositoryProvider : ScriptableObjectProvider<CurrentGameEnvironmentRepository>
    {
        [SerializeField] private RuntimeSelectedGameEnvironment runtimeSelectedGameEnvironment;

        private CurrentGameEnvironmentRepository _cache;

        public override CurrentGameEnvironmentRepository Provide()
        {
            return _cache ?? (_cache = new CurrentGameEnvironmentRepository(runtimeSelectedGameEnvironment));
        }
    }
}