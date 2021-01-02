using System;
using JetBrains.Annotations;
using Noneb.Core.Game.Strongholds;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Noneb.Ui.InGameEditor.Inspector.StrongholdInspector
{
    public class StrongholdInspectorView : MonoBehaviour
    {
        private IDisposable _disposable;

        private StrongholdInspectorViewModel _viewModel;

        [SerializeField] private Toggle toggle;
        [SerializeField] private StrongholdInspectorViewModelFactory viewModelFactory;
        [SerializeField] private GameObject windowGameObject;

        private void OnEnable()
        {
            _viewModel = viewModelFactory.Create();
            _disposable = new CompositeDisposable
            {
                _viewModel.VisibilityLiveData.Subscribe(OnVisibilityChanged),
                _viewModel.StrongholdDataLiveData.Subscribe(OnDataChanged)
            };
        }

        private void OnVisibilityChanged(bool isVisible)
        {
            windowGameObject.SetActive(isVisible);
        }

        private void OnDataChanged([CanBeNull] StrongholdData data)
        {
            toggle.isOn = data != null;
        }

        private void OnDisable()
        {
            _disposable.Dispose();
            _viewModel.Dispose();
        }


        public void ClickedToggle()
        {
            _viewModel.SetIsStronghold(toggle.isOn);
        }
    }
}