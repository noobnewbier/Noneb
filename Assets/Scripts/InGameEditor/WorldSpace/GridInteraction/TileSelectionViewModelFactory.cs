using InGameEditor.Repositories.InGameEditorCamera;
using InGameEditor.Repositories.InGameEditorCurrentHoveredTileHolder;
using Tiles.Holders.Repository;
using UnityEngine;
using UnityUtils.Constants;
using WorldConfigurations.Repositories;

namespace InGameEditor.WorldSpace.GridInteraction
{
    [CreateAssetMenu(fileName = nameof(TileSelectionViewModelFactory), menuName = MenuName.Factory + nameof(TileSelectionViewModel))]
    public class TileSelectionViewModelFactory : ScriptableObject
    {
        [SerializeField] private TileHoldersRepositoryProvider tileHoldersRepositoryProvider;
        [SerializeField] private CurrentWorldConfigRepositoryProvider currentWorldConfigRepositoryProvider;
        [SerializeField] private InGameEditorCurrentlyHoveredTileHolderRepositoryProvider currentlyHoveredTileHolderRepositoryProvider;
        [SerializeField] private InGameEditorCameraRepositoryProvider cameraRepositoryProvider;


        public TileSelectionViewModel Create(Transform mapTransform)
        {
            return new TileSelectionViewModel(
                tileHoldersRepositoryProvider.Provide(),
                currentWorldConfigRepositoryProvider.Provide(),
                currentlyHoveredTileHolderRepositoryProvider.Provide(),
                currentlyHoveredTileHolderRepositoryProvider.Provide(),
                cameraRepositoryProvider.Provide(),
                mapTransform
            );
        }
    }
}