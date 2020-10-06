using Main.Core.Game.WorldConfigurations;
using Main.Ui.Game.Common.UiState.CurrentHoveredTileHolder;
using Main.Ui.Game.Common.UiState.CurrentSelectedTileHolder;
using Main.Ui.Game.GameEnvironments.BoardItemsHoldersFetchingService.Providers;
using Main.Ui.Game.GameEnvironments.Load.Holders.Providers;
using Main.Ui.InGameEditor.Cameras;
using UnityEngine;
using UnityUtils.Constants;

namespace Main.Ui.InGameEditor.WorldSpace.GridInteraction.TileSelection
{
    [CreateAssetMenu(fileName = nameof(TileSelectionViewModelFactory), menuName = MenuName.Factory + nameof(TileSelectionViewModel))]
    public class TileSelectionViewModelFactory : ScriptableObject
    {
        [SerializeField] private CurrentWorldConfigRepositoryProvider currentWorldConfigRepositoryProvider;
        [SerializeField] private CurrentSelectedTileHolderRepositoryProvider currentSelectedTileHolderRepositoryProvider;
        [SerializeField] private CurrentHoveredTileHolderRepositoryProvider currentHoveredTileHolderRepositoryProvider;
        [SerializeField] private TileHoldersFetchingServiceProvider tileHolderRepositoryProvider;
        [SerializeField] private InGameEditorCameraRepositoryProvider cameraRepositoryProvider;
        [SerializeField] private LoadTilesHolderServiceProvider loadTilesHolderServiceProvider;


        public TileSelectionViewModel Create(Transform mapTransform) =>
            new TileSelectionViewModel(
                currentWorldConfigRepositoryProvider.Provide(),
                currentHoveredTileHolderRepositoryProvider.Provide(),
                currentSelectedTileHolderRepositoryProvider.Provide(),
                cameraRepositoryProvider.Provide(),
                mapTransform,
                tileHolderRepositoryProvider.Provide(),
                loadTilesHolderServiceProvider.Provide()
            );
    }
}