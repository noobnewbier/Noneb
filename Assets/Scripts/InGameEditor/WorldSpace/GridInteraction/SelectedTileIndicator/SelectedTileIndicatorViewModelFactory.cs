using Common.Ui.Repository.CurrentSelectedTileHolder;
using UnityEngine;
using UnityUtils.Constants;

namespace InGameEditor.WorldSpace.GridInteraction.SelectedTileIndicator
{
    [CreateAssetMenu(fileName = nameof(SelectedTileIndicatorViewModelFactory), menuName = MenuName.Factory + nameof(SelectedTileIndicatorViewModel))]
    public class SelectedTileIndicatorViewModelFactory : ScriptableObject
    {
        [SerializeField] private CurrentSelectedTileHolderRepositoryProvider repositoryProvider;

        public SelectedTileIndicatorViewModel Create()
        {
            return new SelectedTileIndicatorViewModel(repositoryProvider.Provide());
        }
    }
}