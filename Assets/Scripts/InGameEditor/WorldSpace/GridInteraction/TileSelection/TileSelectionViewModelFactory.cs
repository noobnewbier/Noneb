using Common.Ui.Repository.CurrentHoveredTileHolder;
using Common.Ui.Repository.CurrentSelectedTileHolder;
using GameEnvironments.Common.Repositories.BoardItemsHolders.Providers;
using GameEnvironments.Load.Holders.Providers;
using InGameEditor.Repositories.InGameEditorCamera;
using UnityEngine;
using UnityUtils.Constants;
using WorldConfigurations.Repositories;

namespace InGameEditor.WorldSpace.GridInteraction.TileSelection
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