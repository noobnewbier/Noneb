using Noneb.Core.Game.Common.Constants;
using Noneb.Core.Game.Common.Factories;
using Noneb.Core.Game.Constructs;
using Noneb.Core.InGameEditor.Data;
using Noneb.Ui.InGameEditor.UiState.Inspectable;
using UnityEngine;
using UnityUtils.Constants;

namespace Noneb.Ui.InGameEditor.Inspector.ConstructInspector
{
    [CreateAssetMenu(
        fileName = nameof(ConstructInspectorViewModelFactory),
        menuName = MenuName.Factory + ProjectMenuName.InGameEditor + "ConstructInspectorViewModel"
    )]
    public class ConstructInspectorViewModelFactory : ScriptableObject, IFactory<PresetPaletteInspectorViewModel<PaletteData<Preset<ConstructData>>, ConstructData>>
    {
        [SerializeField] private CurrentInspectableRepositoryProvider currentInspectableRepositoryProvider;

        public PresetPaletteInspectorViewModel<PaletteData<Preset<ConstructData>>, ConstructData> Create() =>
            new PresetPaletteInspectorViewModel<PaletteData<Preset<ConstructData>>, ConstructData>(currentInspectableRepositoryProvider.Provide());
    }
}