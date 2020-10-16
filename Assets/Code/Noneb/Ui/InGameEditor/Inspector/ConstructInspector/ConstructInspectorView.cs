using System;
using Noneb.Core.Game.Constructs;
using Noneb.Core.InGameEditor.Data;
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

        private InspectorViewModel<PaletteData<Preset<ConstructData>>> _viewModel;
        private IDisposable _disposable;

        private void OnEnable()
        {
            _viewModel = viewModelFactory.Create();
            _disposable = new CompositeDisposable
            {
                _viewModel.InspectableLiveData.Subscribe(OnUpdatePreset),
                _viewModel.VisibilityLiveData.Subscribe(OnUpdateVisibility)
            };
        }

        private void OnDisable()
        {
            _disposable.Dispose();
            _viewModel.Dispose();
        }

        private void OnUpdatePreset(PaletteData<Preset<ConstructData>> preset)
        {
            nameText.text = $"Name: {preset.Name}";
            iconImage.sprite = preset.Icon;
        }

        private void OnUpdateVisibility(bool isVisible)
        {
            windowGameObject.SetActive(isVisible);
        }        
    }
}