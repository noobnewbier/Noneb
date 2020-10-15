using Noneb.Core.Game.GameEnvironments.AvailableGameEnvironment;
using Noneb.Core.Game.GameState.CurrentGameEnvironments;
using UnityEngine;
using UnityUtils.Constants;

namespace Noneb.Ui.InGameEditor.EnvironmentSelection
{
    [CreateAssetMenu(
        fileName = nameof(SelectGameEnvironmentViewModelFactory),
        menuName = MenuName.Factory + nameof(SelectGameEnvironmentViewModel)
    )]
    public class SelectGameEnvironmentViewModelFactory : ScriptableObject
    {
        [SerializeField] private AvailableGameEnvironmentRepositoryProvider availableGameEnvironmentRepositoryProvider;
        [SerializeField] private CurrentGameEnvironmentRepositoryProvider currentGameEnvironmentRepositoryProvider;

        public SelectGameEnvironmentViewModel Create() =>
            new SelectGameEnvironmentViewModel(
                availableGameEnvironmentRepositoryProvider.Provide(),
                currentGameEnvironmentRepositoryProvider.Provide()
            );
    }
}