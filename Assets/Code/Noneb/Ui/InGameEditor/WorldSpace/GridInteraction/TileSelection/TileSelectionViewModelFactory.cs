using Noneb.Core.Game.Common.Factories;
using Noneb.Core.Game.GameState.MapConfig;
using Noneb.Ui.Game.UiState.ClickStatus;
using Noneb.Ui.Game.UiState.ClosestTileHolderFromPosition;
using Noneb.Ui.Game.UiState.CurrentHoveredTileHolder;
using Noneb.Ui.Game.UiState.CurrentSelectedTileHolder;
using Noneb.Ui.Game.UiState.MousePositionOnMap;
using Noneb.Ui.InGameEditor.UiState.Inspectable;
using UnityEngine;
using UnityEngine.Serialization;
using UnityUtils.Constants;

namespace Noneb.Ui.InGameEditor.WorldSpace.GridInteraction.TileSelection
{
    [CreateAssetMenu(fileName = nameof(TileSelectionViewModelFactory), menuName = MenuName.Factory + nameof(TileSelectionViewModel))]
    public class TileSelectionViewModelFactory : ScriptableObject, IFactory<TileSelectionViewModel>
    {
        [SerializeField] private CurrentSelectedTileHolderRepositoryProvider currentSelectedTileHolderRepositoryProvider;
        [SerializeField] private CurrentHoveredTileHolderRepositoryProvider currentHoveredTileHolderRepositoryProvider;
        [SerializeField] private CurrentInspectableRepositoryProvider currentInspectableRepositoryProvider;
        [SerializeField] private ClosestTileHolderFromPositionServiceProvider closestTileHolderFromPositionServiceProvider;
        [FormerlySerializedAs("mousePositionServiceProvider")] [SerializeField] private MousePositionOnMapServiceProvider mousePositionOnMapServiceProvider;
        [SerializeField] private MapConfigRepositoryProvider loadedMapConfigRepositoryProvider;
        [SerializeField] private ClickStatusServiceProvider clickStatusServiceProvider;
        

        public TileSelectionViewModel Create() =>
            new TileSelectionViewModel(
                currentHoveredTileHolderRepositoryProvider.Provide(),
                currentSelectedTileHolderRepositoryProvider.Provide(),
                currentInspectableRepositoryProvider.Provide(),
                closestTileHolderFromPositionServiceProvider.Provide(),
                mousePositionOnMapServiceProvider.Provide(),
                loadedMapConfigRepositoryProvider.Provide(),
                clickStatusServiceProvider.Provide()
            );
    }
}