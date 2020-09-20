using Common.Ui.Repository.CurrentHoveredTileHolder;
using Common.Ui.Repository.CurrentSelectedTileHolder;
using InGameEditor.Repositories.InGameEditorCamera;
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
        [FormerlySerializedAs("tileHoldersRepositoryProvider")] [SerializeField] private TilesHoldersServiceProvider tilesHoldersServiceProvider;
        [SerializeField] private CurrentWorldConfigRepositoryProvider currentWorldConfigRepositoryProvider;
        [SerializeField] private CurrentSelectedTileHolderRepositoryProvider currentSelectedTileHolderRepositoryProvider;

        [FormerlySerializedAs("currentlyHoveredTileHolderRepositoryProvider")] [SerializeField]
        private CurrentHoveredTileHolderRepositoryProvider currentHoveredTileHolderRepositoryProvider;

        [SerializeField] private InGameEditorCameraRepositoryProvider cameraRepositoryProvider;


        public TileSelectionViewModel Create(Transform mapTransform)
        {
            return new TileSelectionViewModel(
                tilesHoldersServiceProvider.Provide(),
                currentWorldConfigRepositoryProvider.Provide(),
                currentHoveredTileHolderRepositoryProvider.Provide(),
                currentSelectedTileHolderRepositoryProvider.Provide(),
                cameraRepositoryProvider.Provide(),
                mapTransform
            );
        }
    }
}