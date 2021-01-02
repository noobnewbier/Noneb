using Noneb.Core.Game.Common.Constants;
using Noneb.Core.Game.Common.Providers;
using Noneb.Core.Game.Coordinates;
using UnityEngine;
using UnityUtils.Constants;

namespace Noneb.Core.InGameEditor.LevelDataEditing
{
    [CreateAssetMenu(
        fileName = nameof(LevelDataEditingServiceProvider),
        menuName = MenuName.ScriptableService + ProjectMenuName.InGameEditor + nameof(LevelDataEditingService)
    )]
    public class LevelDataEditingServiceProvider : ScriptableObject, IObjectProvider<ILevelDataEditingService>
    {
        [SerializeField] private CoordinateServiceProvider coordinateServiceProvider;
        private ILevelDataEditingService _cache;

        public ILevelDataEditingService Provide() => _cache ?? (_cache = new LevelDataEditingService(coordinateServiceProvider.Provide()));
    }
}