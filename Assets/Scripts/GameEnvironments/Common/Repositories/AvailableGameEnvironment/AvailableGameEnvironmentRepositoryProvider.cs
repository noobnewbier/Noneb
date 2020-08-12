using System.Collections.Generic;
using Common.Providers;
using GameEnvironments.Common.Data;
using UnityEngine;
using UnityUtils.Constants;

namespace GameEnvironments.Common.Repositories.AvailableGameEnvironment
{
    [CreateAssetMenu(
        fileName = nameof(AvailableGameEnvironmentRepositoryProvider),
        menuName = MenuName.ScriptableRepository + nameof(AvailableGameEnvironmentRepository)
    )]
    public class AvailableGameEnvironmentRepositoryProvider : ScriptableObjectProvider<IAvailableGameEnvironmentRepository>
    {
        [SerializeField] private List<GameEnvironmentScriptable> gameEnvironmentScriptables;

        private IAvailableGameEnvironmentRepository _cache;

        public override IAvailableGameEnvironmentRepository Provide()
        {
            return _cache ?? (_cache = new AvailableGameEnvironmentRepository(gameEnvironmentScriptables));
        }
    }
}