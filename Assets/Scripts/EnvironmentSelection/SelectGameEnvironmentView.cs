using System.Collections.Generic;
using EnvironmentSelection.ClickableGameEnvironments;
using GameEnvironments.Common.Data;
using TMPro;
using UnityEngine;

namespace EnvironmentSelection
{
    public class SelectGameEnvironmentView : MonoBehaviour
    {
        [SerializeField] private SelectGameEnvironmentViewModelFactory viewModelFactory;
        [SerializeField] private TextMeshProUGUI selectedEnvironmentNameText;
        [SerializeField] private Transform gameEnvironmentsParentTransform;
        [SerializeField] private ClickableGameEnvironmentViewProvider clickableGameEnvironmentViewProvider;

        private ISelectGameEnvironmentViewModel _viewModel;

        private void OnEnable()
        {
            _viewModel = viewModelFactory.Create();

            _viewModel.SelectedGameEnvironmentLiveData.Subscribe(ShowSelectedGameEnvironment);
            _viewModel.AvailableGameEnvironmentLiveData.Subscribe(ShowAvailableGameEnvironmentList);

            _viewModel.RefreshAvailableGameEnvironmentList();
        }

        private void ShowSelectedGameEnvironment(GameEnvironment gameEnvironment)
        {
            selectedEnvironmentNameText.text = gameEnvironment.EnvironmentName;
        }

        private void ShowAvailableGameEnvironmentList(IEnumerable<GameEnvironment> gameEnvironments)
        {
            //Kill all and than instantiate new ones, not efficient but will work for now
            foreach (Transform child in gameEnvironmentsParentTransform) Destroy(child);

            foreach (var environment in gameEnvironments)
            {
                var goAndComponent = clickableGameEnvironmentViewProvider.Provide(gameEnvironmentsParentTransform, false);
                goAndComponent.Component.Instantiate(environment, _viewModel.SelectGameEnvironment);
            }
        }
    }
}