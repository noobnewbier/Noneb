using System;
using Noneb.Core.Game.Common.Providers;
using UnityEngine;
using UnityUtils.Constants;

namespace Noneb.Core.Game.GameEnvironments.Validation
{
    [CreateAssetMenu(
        fileName = nameof(LevelDataScriptableValidationServiceProvider),
        menuName = MenuName.ScriptableService + nameof(LevelDataScriptableValidationService)
    )]
    public class LevelDataScriptableValidationServiceProvider : ScriptableObject, IObjectProvider<ILevelDataScriptableValidationService>
    {
        private readonly Lazy<ILevelDataScriptableValidationService>
            _lazyInstance = new Lazy<ILevelDataScriptableValidationService>(() => new LevelDataScriptableValidationService());

        public ILevelDataScriptableValidationService Provide() => _lazyInstance.Value;
    }
}