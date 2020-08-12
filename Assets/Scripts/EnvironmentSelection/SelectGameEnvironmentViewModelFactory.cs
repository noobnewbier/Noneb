using GameEnvironments.Common.Repositories.AvailableGameEnvironment;
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

        public ISelectGameEnvironmentViewModel Create()
        {
            return new SelectGameEnvironmentViewModel(availableGameEnvironmentRepositoryProvider.Provide());
        }
    }
}