﻿using System;
using System.Collections.Generic;
using Noneb.Core.Game.GameEnvironments.Data;
using Noneb.Ui.InGameEditor.EnvironmentSelection.ClickableGameEnvironments;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;

namespace Noneb.Ui.InGameEditor.EnvironmentSelection
{
    public class SelectGameEnvironmentView : MonoBehaviour
    {
        [SerializeField] private SelectGameEnvironmentViewModelFactory viewModelFactory;
        [SerializeField] private TextMeshProUGUI selectedEnvironmentNameText;
        [SerializeField] private Transform gameEnvironmentsParentTransform;

        [FormerlySerializedAs("clickableGameEnvironmentViewProvider")] [SerializeField]
        private ClickableGameEnvironmentViewFactory clickableGameEnvironmentViewFactory;

        private SelectGameEnvironmentViewModel _viewModel;
        private IDisposable _compositeDisposable;

        private void OnEnable()
        {
            _viewModel = viewModelFactory.Create();

            _compositeDisposable = new CompositeDisposable
            {
                _viewModel.CurrentlyInspectingGameEnvironmentLiveData.Subscribe(ShowSelectedGameEnvironment),
                _viewModel.AvailableGameEnvironmentLiveData.Subscribe(ShowAvailableGameEnvironmentList)
            };
        }

        private void OnDisable()
        {
            _compositeDisposable?.Dispose();
            _viewModel.Dispose();
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
                var goAndComponent = clickableGameEnvironmentViewFactory.Create(gameEnvironmentsParentTransform, false);
                goAndComponent.component.Init(environment, _viewModel.InspectGameEnvironment);
            }
        }

        public void SelectCurrentlyInspectedEnvironment()
        {
            _viewModel.SelectCurrentlyInspectedGameEnvironment();
        }
    }
}