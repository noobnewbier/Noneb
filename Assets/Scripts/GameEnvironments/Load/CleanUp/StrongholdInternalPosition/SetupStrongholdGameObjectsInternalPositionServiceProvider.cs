using System;
using Common.Providers;
using GameEnvironments.Load.CleanUp.StrongholdInternalPosition;
using UnityEngine;
using UnityUtils.Constants;

namespace GameEnvironments.Load.Obsolete.BoardItemOnTile.StrongholdInternalPosition
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