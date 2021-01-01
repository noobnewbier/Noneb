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
        fileName = nameof(TileInspectorViewModelFactory),
        menuName = MenuName.Factory + ProjectMenuName.InGameEditor + "TileInspectorViewModel"
    )]
    public class TileInspectorViewModelFactory : ScriptableObject,
                                                 IFactory<BoardItemInspectorViewModel<Tile, TileData>>
    {
        [SerializeField] private CurrentInspectableRepositoryProvider currentInspectableRepositoryProvider;
        [SerializeField] private MapRepositoryProvider mapRepositoryProvider;


        public BoardItemInspectorViewModel<Tile, TileData> Create() =>
            new BoardItemInspectorViewModel<Tile, TileData>(
                currentInspectableRepositoryProvider.Provide(),
                mapRepositoryProvider.Provide()
            );
    }
}