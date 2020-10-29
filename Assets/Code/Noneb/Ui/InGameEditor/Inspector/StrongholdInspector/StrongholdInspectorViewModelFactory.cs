using Noneb.Core.Game.Common.Constants;
using Noneb.Core.Game.Common.Factories;
using Noneb.Core.Game.GameState.Maps;
using Noneb.Core.InGameEditor.LevelEdit;
using Noneb.Ui.InGameEditor.UiState.Inspectable;
using UnityEngine;
using UnityUtils.Constants;

namespace Noneb.Ui.InGameEditor.Inspector.StrongholdInspector
{
    [CreateAssetMenu(
        fileName = nameof(StrongholdInspectorViewModelFactory),
        menuName = MenuName.Factory + ProjectMenuName.InGameEditor + nameof(StrongholdInspectorViewModel)
    )]
    public class StrongholdInspectorViewModelFactory : ScriptableObject, IFactory<StrongholdInspectorViewModel>
    {
        [SerializeField] private CurrentInspectableRepositoryProvider currentInspectableRepositoryProvider;
        [SerializeField] private MapRepositoryProvider mapRepositoryProvider;
        [SerializeField] private LevelEditServiceProvider levelEditServiceProvider;

        public StrongholdInspectorViewModel Create() => new StrongholdInspectorViewModel(
            currentInspectableRepositoryProvider.Provide(),
            mapRepositoryProvider.Provide(),
            levelEditServiceProvider.Provide()
        );
    }
}