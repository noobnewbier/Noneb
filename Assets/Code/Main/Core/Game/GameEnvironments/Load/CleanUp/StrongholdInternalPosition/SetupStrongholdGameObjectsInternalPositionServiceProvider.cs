﻿using System;
using Common.Providers;
using UnityEngine;
using UnityUtils.Constants;

namespace GameEnvironments.Load.CleanUp.StrongholdInternalPosition
{
    [CreateAssetMenu(
        fileName = nameof(SetupStrongholdGameObjectsInternalPositionServiceProvider),
        menuName = MenuName.ScriptableService + nameof(SetupStrongholdGameObjectsInternalPositionService)
    )]
    public class SetupStrongholdGameObjectsInternalPositionServiceProvider : ScriptableObjectProvider<
        ISetupStrongholdGameObjectsInternalPositionService>
    {
        private readonly Lazy<ISetupStrongholdGameObjectsInternalPositionService> _lazyInstance =
            new Lazy<ISetupStrongholdGameObjectsInternalPositionService>(() => new SetupStrongholdGameObjectsInternalPositionService());

        public override ISetupStrongholdGameObjectsInternalPositionService Provide()
        {
            return _lazyInstance.Value;
        }
    }
}