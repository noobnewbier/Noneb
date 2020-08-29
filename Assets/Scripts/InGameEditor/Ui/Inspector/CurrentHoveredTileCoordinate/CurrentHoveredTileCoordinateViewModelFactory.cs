using InGameEditor.Repositories.InGameEditorCurrentHoveredTileHolder;
using UnityEngine;
using UnityUtils.Constants;

namespace InGameEditor.Ui.Inspector.CurrentHoveredTileCoordinate
{
    [CreateAssetMenu(
        fileName = nameof(CurrentHoveredTileCoordinateViewModelFactory),
        menuName = MenuName.Factory + nameof(CurrentHoveredTileCoordinateViewModel)
    )]
    public class CurrentHoveredTileCoordinateViewModelFactory : ScriptableObject
    {
        [SerializeField] private InGameEditorCurrentlyHoveredTileHolderRepositoryProvider repositoryProvider;

        public CurrentHoveredTileCoordinateViewModel Create()
        {
            return new CurrentHoveredTileCoordinateViewModel(repositoryProvider.Provide());
        }
    }
}