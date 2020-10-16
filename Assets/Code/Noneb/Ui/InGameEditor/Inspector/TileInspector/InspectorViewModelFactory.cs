using Noneb.Core.Game.Common.Constants;
using Noneb.Core.Game.Common.Factories;
using Noneb.Core.Game.Tiles;
using Noneb.Core.InGameEditor.Data;
using Noneb.Ui.InGameEditor.UiState.Inspectable;
using UnityEngine;
using UnityUtils.Constants;

namespace Noneb.Ui.InGameEditor.Inspector.TileInspector
{
    [CreateAssetMenu(fileName = nameof(InspectorViewModelFactory), menuName = MenuName.Factory + ProjectMenuName.InGameEditor + "InspectorViewModel")]
    public class InspectorViewModelFactory : ScriptableObject, IFactory<InspectorViewModel<PaletteData<Preset<TileData>>>>
    {
        [SerializeField] private CurrentInspectableRepositoryProvider currentInspectableRepositoryProvider;
        
        public InspectorViewModel<PaletteData<Preset<TileData>>> Create() => new InspectorViewModel<PaletteData<Preset<TileData>>>(currentInspectableRepositoryProvider.Provide());
    }
}