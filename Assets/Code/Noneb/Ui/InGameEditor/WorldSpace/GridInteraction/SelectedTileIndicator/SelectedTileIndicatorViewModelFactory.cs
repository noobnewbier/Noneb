using Noneb.Ui.Game.UiState.CurrentSelectedTileHolder;
using UnityEngine;
using UnityUtils.Constants;

namespace Noneb.Ui.InGameEditor.WorldSpace.GridInteraction.SelectedTileIndicator
{
    [CreateAssetMenu(fileName = nameof(SelectedTileIndicatorViewModelFactory), menuName = MenuName.Factory + nameof(SelectedTileIndicatorViewModel))]
    public class SelectedTileIndicatorViewModelFactory : ScriptableObject
    {
        [SerializeField] private CurrentSelectedTileHolderRepositoryProvider repositoryProvider;

        public SelectedTileIndicatorViewModel Create() => new SelectedTileIndicatorViewModel(repositoryProvider.Provide());
    }
}