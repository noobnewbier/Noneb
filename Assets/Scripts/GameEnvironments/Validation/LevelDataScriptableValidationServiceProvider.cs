﻿using System;
using Common.Providers;
using UnityEngine;
using UnityUtils.Constants;

namespace GameEnvironments.Validation
{
    [CreateAssetMenu(
        fileName = nameof(LevelDataScriptableValidationServiceProvider),
        menuName = MenuName.ScriptableService + nameof(LevelDataScriptableValidationService)
    )]
    public class LevelDataScriptableValidationServiceProvider : ScriptableObjectProvider<LevelDataScriptableValidationService>
    {
        private readonly Lazy<LevelDataScriptableValidationService>
            _lazyInstance = new Lazy<LevelDataScriptableValidationService>(() => new LevelDataScriptableValidationService());

        public override LevelDataScriptableValidationService Provide()
        {
            return _lazyInstance.Value;
        }
    }
}