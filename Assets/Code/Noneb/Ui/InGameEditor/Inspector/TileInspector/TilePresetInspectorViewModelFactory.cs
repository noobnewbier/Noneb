using Noneb.Core.Game.Common.Constants;
using Noneb.Core.Game.Common.Factories;
using Noneb.Core.Game.Tiles;
using Noneb.Core.InGameEditor.Data;
using Noneb.Ui.InGameEditor.UiState.Inspectable;
using UnityEngine;
using UnityUtils.Constants;

namespace Noneb.Ui.InGameEditor.Inspector.TileInspector
{
    [CreateAssetMenu(
        fileName = nameof(TilePresetInspectorViewModelFactory),
        menuName = MenuName.Factory + ProjectMenuName.InGameEditor + "TilePresetInspectorViewModel"
    )]
    public class TilePresetInspectorViewModelFactory : ScriptableObject,
                                                       IFactory<PresetPaletteInspectorViewModel<PaletteData<Preset<TileData>>, TileData>>
    {
        [SerializeField] private CurrentInspectableRepositoryProvider currentInspectableRepositoryProvider;

        public PresetPaletteInspectorViewModel<PaletteData<Preset<TileData>>, TileData> Create() =>
            new PresetPaletteInspectorViewModel<PaletteData<Preset<TileData>>, TileData>(currentInspectableRepositoryProvider.Provide());
    }
}