using Noneb.Core.Game.Common.Constants;
using Noneb.Core.Game.Common.Factories;
using Noneb.Core.Game.GameState.Maps;
using Noneb.Core.Game.Units;
using Noneb.Ui.InGameEditor.UiState.Inspectable;
using UnityEngine;
using UnityUtils.Constants;

namespace Noneb.Ui.InGameEditor.Inspector.UnitInspector
{
    [CreateAssetMenu(
        fileName = nameof(UnitInspectorViewModelFactory),
        menuName = MenuName.Factory + ProjectMenuName.InGameEditor + "UnitInspectorViewModelFactory"
    )]
    public class UnitInspectorViewModelFactory : ScriptableObject, IFactory<InspectorViewModel<Unit, UnitData>>
    {
        [SerializeField] private CurrentInspectableRepositoryProvider currentInspectableRepositoryProvider;
        [SerializeField] private MapRepositoryProvider mapRepositoryProvider;


        public InspectorViewModel<Unit, UnitData> Create() =>
            new InspectorViewModel<Unit, UnitData>(currentInspectableRepositoryProvider.Provide(), mapRepositoryProvider.Provide());
    }
}