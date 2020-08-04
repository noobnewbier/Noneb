using Common.Providers;
using GameEnvironments.Common.Data;
using UnityEngine;

namespace GameEnvironments.Common.Repositories.CurrentGameEnvironment
{
    public class CurrentGameEnvironmentRepositoryProvider : MonoObjectProvider<ICurrentGameEnvironmentRepository>
    {
        // At some point this might not be serialized,
        // as it should be "loaded"(set) by some script instead of a human 
        [SerializeField] private GameEnvironmentScriptable gameEnvironmentScriptable;

        private ICurrentGameEnvironmentRepository _cache;

        public override ICurrentGameEnvironmentRepository Provide()
        {
            return _cache ?? (_cache = new CurrentGameEnvironmentRepository(gameEnvironmentScriptable.ToGameEnvironment()));
        }
    }
}