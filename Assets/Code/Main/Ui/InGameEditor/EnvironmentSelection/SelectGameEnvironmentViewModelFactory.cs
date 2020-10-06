using Main.Core.Game.GameEnvironments.AvailableGameEnvironment;
using Main.Core.Game.GameEnvironments.CurrentGameEnvironments;
using UnityEngine;
using UnityUtils.Constants;

namespace Main.Ui.InGameEditor.EnvironmentSelection
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