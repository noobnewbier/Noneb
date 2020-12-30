using Noneb.Core.Game.Common.Constants;
using Noneb.Core.Game.Common.Providers;
using Noneb.Core.Game.GameState.LevelDatas;
using Noneb.Core.Game.GameState.MapConfigs;
using Noneb.Core.Game.GameState.Maps;
using Noneb.Core.Game.Maps.MapModification;
using Noneb.Core.InGameEditor.LevelDataModification;
using UnityEngine;
using UnityUtils.Constants;

namespace Noneb.Core.InGameEditor.LevelEdit
{
    [CreateAssetMenu(
        fileName = nameof(LevelEditServiceProvider),
        menuName = MenuName.ScriptableService + ProjectMenuName.InGameEditor + nameof(LevelEditService)
    )]
    public class LevelEditServiceProvider : ScriptableObject, IObjectProvider<ILevelEditService>
    {
        [SerializeField] private LevelDataModificationServiceProvider levelDataModificationServiceProvider;
        [SerializeField] private MapModificationServiceProvider mapModificationServiceProvider;
        [SerializeField] private MapConfigRepositoryProvider mapConfigRepositoryProvider;
        [SerializeField] private MapRepositoryProvider mapRepositoryProvider;
        [SerializeField] private LevelDataRepositoryProvider levelDataRepositoryProvider;

        private ILevelEditService _cache;

        public ILevelEditService Provide() => _cache ?? (_cache = new LevelEditService(
            levelDataModificationServiceProvider.Provide(),
            mapModificationServiceProvider.Provide(),
            mapConfigRepositoryProvider.Provide(),
            mapRepositoryProvider.Provide(),
            levelDataRepositoryProvider.Provide()
        ));
    }
}