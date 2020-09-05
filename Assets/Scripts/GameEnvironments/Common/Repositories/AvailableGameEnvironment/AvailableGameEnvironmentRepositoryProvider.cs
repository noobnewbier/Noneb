using System.Collections.Generic;
using System.Linq;
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
            if (_cache ==null)
            {
                CreateCache();
            }
            
            return _cache;
        }

        private void CreateCache()
        {
            _cache = new AvailableGameEnvironmentRepository();
            
            _cache.Set(gameEnvironmentScriptables.Select(s => s.ToGameEnvironment()));
        }
    }
}