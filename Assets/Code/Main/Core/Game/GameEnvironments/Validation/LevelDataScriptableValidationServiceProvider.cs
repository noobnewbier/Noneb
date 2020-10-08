using System;
using Main.Core.Game.Common.Providers;
using UnityEngine;
using UnityUtils.Constants;

namespace Main.Core.Game.GameEnvironments.Validation
{
    [CreateAssetMenu(
        fileName = nameof(LevelDataScriptableValidationServiceProvider),
        menuName = MenuName.ScriptableService + nameof(LevelDataScriptableValidationService)
    )]
    public class LevelDataScriptableValidationServiceProvider : ScriptableObject, IObjectProvider<LevelDataScriptableValidationService>
    {
        private readonly Lazy<LevelDataScriptableValidationService>
            _lazyInstance = new Lazy<LevelDataScriptableValidationService>(() => new LevelDataScriptableValidationService());

        public LevelDataScriptableValidationService Provide() => _lazyInstance.Value;
    }
}