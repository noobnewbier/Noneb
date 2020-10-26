using System;
using Noneb.Core.Game.Constructs;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Noneb.Ui.InGameEditor.Inspector.ConstructInspector
{
    public class ConstructInspectorView : MonoBehaviour
    {
        [SerializeField] private GameObject windowGameObject;

        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private Image iconImage;

        [SerializeField] private ConstructInspectorViewModelFactory viewModelFactory;

        private InspectorViewModel<Construct, ConstructData> _viewModel;
        private IDisposable _disposable;

        private void OnEnable()
        {
            _viewModel = viewModelFactory.Create();
            _disposable = new CompositeDisposable
            {
                _viewModel.TypeTLiveData.Subscribe(OnUpdatePreset),
                _viewModel.VisibilityLiveData.Subscribe(OnUpdateVisibility)
            };
        }

        private void OnDisable()
        {
            _disposable.Dispose();
            _viewModel.Dispose();
        }

        private void OnUpdatePreset(ConstructData data)
        {
            nameText.text = $"Name: {data.Name}";
            iconImage.sprite = data.Icon;
        }

        private void OnUpdateVisibility(bool isVisible)
        {
            windowGameObject.SetActive(isVisible);
        }
    }
}