using InGameEditor.Repositories.InGameEditorCamera;
using InGameEditor.Repositories.InGameEditorCurrentHoveredTileHolder;
using InGameEditor.Repositories.InGameEditorCurrentSelectedTileHolder;
using Tiles.Holders.Repository;
using UnityEngine;
using UnityEngine.Serialization;
using UnityUtils.Constants;
using WorldConfigurations.Repositories;

namespace InGameEditor.WorldSpace.GridInteraction.TileSelection
{
    [CreateAssetMenu(fileName = nameof(TileSelectionViewModelFactory), menuName = MenuName.Factory + nameof(TileSelectionViewModel))]
    public class TileSelectionViewModelFactory : ScriptableObject
    {
        [SerializeField] private TileHoldersRepositoryProvider tileHoldersRepositoryProvider;
        [SerializeField] private CurrentWorldConfigRepositoryProvider currentWorldConfigRepositoryProvider;
        [SerializeField] private InGameEditorCurrentSelectedTileHolderRepositoryProvider currentSelectedTileHolderRepositoryProvider;

        [FormerlySerializedAs("currentlyHoveredTileHolderRepositoryProvider")] [SerializeField]
        private InGameEditorCurrentHoveredTileHolderRepositoryProvider currentHoveredTileHolderRepositoryProvider;

        [SerializeField] private InGameEditorCameraRepositoryProvider cameraRepositoryProvider;


        public TileSelectionViewModel Create(Transform mapTransform)
        {
            return new TileSelectionViewModel(
                tileHoldersRepositoryProvider.Provide(),
                currentWorldConfigRepositoryProvider.Provide(),
                currentHoveredTileHolderRepositoryProvider.Provide(),
                currentSelectedTileHolderRepositoryProvider.Provide(),
                cameraRepositoryProvider.Provide(),
                mapTransform
            );
        }
    }
}