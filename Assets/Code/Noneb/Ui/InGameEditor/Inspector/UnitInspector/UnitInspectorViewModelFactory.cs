using Noneb.Core.Game.Common.Constants;
using Noneb.Core.Game.Common.Factories;
using Noneb.Core.Game.Units;
using Noneb.Core.InGameEditor.Data;
using Noneb.Ui.InGameEditor.UiState.Inspectable;
using UnityEngine;
using UnityUtils.Constants;

namespace Noneb.Ui.InGameEditor.Inspector.UnitInspector
{
    [CreateAssetMenu(
        fileName = nameof(UnitInspectorViewModelFactory),
        menuName = MenuName.Factory + ProjectMenuName.InGameEditor + "UnitInspectorViewModelFactory"
    )]
    public class UnitInspectorViewModelFactory : ScriptableObject, IFactory<InspectorViewModel<PaletteData<Preset<UnitData>>>>
    {
        [SerializeField] private CurrentInspectableRepositoryProvider currentInspectableRepositoryProvider;

        public InspectorViewModel<PaletteData<Preset<UnitData>>> Create() =>
            new InspectorViewModel<PaletteData<Preset<UnitData>>>(currentInspectableRepositoryProvider.Provide());
    }
}