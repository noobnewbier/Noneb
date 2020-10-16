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
    public class ConstructInspectorViewModelFactory : ScriptableObject, IFactory<InspectorViewModel<PaletteData<Preset<ConstructData>>>>
    {
        [SerializeField] private CurrentInspectableRepositoryProvider currentInspectableRepositoryProvider;

        public InspectorViewModel<PaletteData<Preset<ConstructData>>> Create() =>
            new InspectorViewModel<PaletteData<Preset<ConstructData>>>(currentInspectableRepositoryProvider.Provide());
    }
}