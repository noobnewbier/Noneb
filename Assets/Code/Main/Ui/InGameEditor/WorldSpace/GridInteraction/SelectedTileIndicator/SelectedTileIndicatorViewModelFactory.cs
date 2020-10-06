using Main.Ui.Game.Common.UiState.CurrentSelectedTileHolder;
using UnityEngine;
using UnityUtils.Constants;

namespace Main.Ui.InGameEditor.WorldSpace.GridInteraction.SelectedTileIndicator
{
    [CreateAssetMenu(fileName = nameof(SelectedTileIndicatorViewModelFactory), menuName = MenuName.Factory + nameof(SelectedTileIndicatorViewModel))]
    public class SelectedTileIndicatorViewModelFactory : ScriptableObject
    {
        [SerializeField] private CurrentSelectedTileHolderRepositoryProvider repositoryProvider;

        public SelectedTileIndicatorViewModel Create() => new SelectedTileIndicatorViewModel(repositoryProvider.Provide());
    }
}