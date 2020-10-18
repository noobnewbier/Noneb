using Noneb.Core.Game.Common.Constants;
using Noneb.Core.Game.Common.Factories;
using Noneb.Core.Game.GameState.Maps;
using Noneb.Core.Game.Tiles;
using Noneb.Ui.InGameEditor.UiState.Inspectable;
using UnityEngine;
using UnityUtils.Constants;

namespace Noneb.Ui.InGameEditor.Inspector.TileInspector
{
    [CreateAssetMenu(
        fileName = nameof(CoordinateTileInspectorViewModelFactory),
        menuName = MenuName.Factory + ProjectMenuName.InGameEditor + "CoordinateTileInspectorViewModel"
    )]
    public class CoordinateTileInspectorViewModelFactory : ScriptableObject, IFactory<CoordinateInspectorViewModel<Tile, TileData>>
    {
        [SerializeField] private CurrentInspectableRepositoryProvider currentInspectableRepositoryProvider;
        [SerializeField] private MapRepositoryProvider mapRepositoryProvider;

        public CoordinateInspectorViewModel<Tile, TileData> Create() => new CoordinateInspectorViewModel<Tile, TileData>(
            currentInspectableRepositoryProvider.Provide(),
            mapRepositoryProvider.Provide()
        );
    }
}