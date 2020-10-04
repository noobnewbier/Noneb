using GameEnvironments.Common.Repositories.AvailableGameEnvironment;
using GameEnvironments.Common.Repositories.CurrentGameEnvironments;
using UnityEngine;
using UnityUtils.Constants;

namespace EnvironmentSelection
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