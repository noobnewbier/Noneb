using System;
using Noneb.Core.Game.Common.Providers;
using UnityEngine;
using UnityUtils.Constants;

namespace Noneb.Ui.Game.GameEnvironments.Load.CleanUp.StrongholdInternalPosition
{
    [CreateAssetMenu(
        fileName = nameof(SetupStrongholdGameObjectsInternalPositionServiceProvider),
        menuName = MenuName.ScriptableService + nameof(SetupStrongholdGameObjectsInternalPositionService)
    )]
    public class SetupStrongholdGameObjectsInternalPositionServiceProvider : ScriptableObject,
                                                                             IObjectProvider<ISetupStrongholdGameObjectsInternalPositionService>
    {
        private readonly Lazy<ISetupStrongholdGameObjectsInternalPositionService> _lazyInstance =
            new Lazy<ISetupStrongholdGameObjectsInternalPositionService>(() => new SetupStrongholdGameObjectsInternalPositionService());

        public ISetupStrongholdGameObjectsInternalPositionService Provide() => _lazyInstance.Value;
    }
}