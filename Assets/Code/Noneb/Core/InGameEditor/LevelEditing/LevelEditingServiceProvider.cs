using Noneb.Core.Game.Common.Constants;
using Noneb.Core.Game.Common.Providers;
using Noneb.Core.Game.GameState.LevelDatas;
using Noneb.Core.Game.GameState.MapConfigs;
using Noneb.Core.Game.GameState.Maps;
using Noneb.Core.Game.Maps.MapModification;
using Noneb.Core.InGameEditor.LevelDataEditing;
using UnityEngine;
using UnityEngine.Serialization;
using UnityUtils.Constants;

namespace Noneb.Core.InGameEditor.LevelEditing
{
    [CreateAssetMenu(
        fileName = nameof(LevelEditingServiceProvider),
        menuName = MenuName.ScriptableService + ProjectMenuName.InGameEditor + nameof(LevelEditingService)
    )]
    public class LevelEditingServiceProvider : ScriptableObject, IObjectProvider<ILevelEditingService>
    {
        [FormerlySerializedAs("levelDataModificationServiceProvider")] [SerializeField] private LevelDataEditingServiceProvider levelDataEditingServiceProvider;
        [FormerlySerializedAs("mapModificationServiceProvider")] [SerializeField] private MapEditingServiceProvider mapEditingServiceProvider;
        [SerializeField] private MapConfigRepositoryProvider mapConfigRepositoryProvider;
        [SerializeField] private MapRepositoryProvider mapRepositoryProvider;
        [SerializeField] private LevelDataRepositoryProvider levelDataRepositoryProvider;

        private ILevelEditingService _cache;

        public ILevelEditingService Provide() => _cache ?? (_cache = new LevelEditingService(
            levelDataEditingServiceProvider.Provide(),
            mapEditingServiceProvider.Provide(),
            mapConfigRepositoryProvider.Provide(),
            mapRepositoryProvider.Provide(),
            levelDataRepositoryProvider.Provide()
        ));
    }
}