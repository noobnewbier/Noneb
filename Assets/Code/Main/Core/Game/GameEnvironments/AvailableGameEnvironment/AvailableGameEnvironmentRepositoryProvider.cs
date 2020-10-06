using System.Collections.Generic;
using System.Data;
using System.Linq;
using Main.Core.Game.Common.Providers;
using Main.Core.Game.GameEnvironments.Data;
using Main.Core.Game.GameEnvironments.Validation;
using UnityEngine;
using UnityEngine.Serialization;
using UnityUtils.Constants;

namespace Main.Core.Game.GameEnvironments.AvailableGameEnvironment
{
    [CreateAssetMenu(
        fileName = nameof(AvailableGameEnvironmentRepositoryProvider),
        menuName = MenuName.ScriptableRepository + nameof(AvailableGameEnvironmentRepository)
    )]
    public class AvailableGameEnvironmentRepositoryProvider : ScriptableObjectProvider<IAvailableGameEnvironmentRepository>
    {
        [SerializeField] private List<GameEnvironmentScriptable> gameEnvironmentScriptables;

        [FormerlySerializedAs("levelDataValidationServiceProvider")] [SerializeField]
        private LevelDataScriptableValidationServiceProvider levelDataScriptableValidationServiceProvider;

        private IAvailableGameEnvironmentRepository _cache;

        public override IAvailableGameEnvironmentRepository Provide()
        {
            if (_cache == null)
            {
                CreateCache();
            }

            return _cache;
        }

        private void CreateCache()
        {
            ValidateEnvironmentLevelDatas();

            _cache = new AvailableGameEnvironmentRepository();
            _cache.Set(gameEnvironmentScriptables.Select(s => s.ToGameEnvironment()));
        }

        [ContextMenu(nameof(ValidateEnvironmentLevelDatas))]
        private void ValidateEnvironmentLevelDatas()
        {
            var validationService = levelDataScriptableValidationServiceProvider.Provide();
            foreach (var env in gameEnvironmentScriptables)
            {
                var result = validationService.Validate(env.LevelData, env.MapConfiguration);
                if (!result.IsValid)
                {
                    Debug.Log(env.EnvironmentName + " : is not valid");
                    foreach (var reason in result.FailedReason) Debug.Log(reason);
#if UNITY_EDITOR
                    throw new DataException(env.EnvironmentName + " : is not valid");
#endif
                }
            }
        }
    }
}