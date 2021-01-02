using Noneb.Core.Game.Common.Constants;
using Noneb.Core.Game.Common.Providers;
using Noneb.Core.Game.Coordinates;
using UnityEngine;
using UnityUtils.Constants;

namespace Noneb.Core.InGameEditor.LevelDataEditing
{
    [CreateAssetMenu(
        fileName = nameof(LevelDataModificationServiceProvider),
        menuName = MenuName.ScriptableService + ProjectMenuName.InGameEditor + nameof(LevelDataModificationService)
    )]
    public class LevelDataModificationServiceProvider : ScriptableObject, IObjectProvider<ILevelDataModificationService>
    {
        [SerializeField] private CoordinateServiceProvider coordinateServiceProvider;
        private ILevelDataModificationService _cache;

        public ILevelDataModificationService Provide() => _cache ?? (_cache = new LevelDataModificationService(coordinateServiceProvider.Provide()));
    }
}