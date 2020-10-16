using System;
using System.Globalization;
using Noneb.Core.Game.Units;
using Noneb.Core.InGameEditor.Data;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Noneb.Ui.InGameEditor.Inspector.UnitInspector
{
    public class UnitInspectorView : MonoBehaviour
    {
        [SerializeField] private GameObject windowGameObject;

        [SerializeField] private UnitInspectorViewModelFactory viewModelFactory;
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI healthText;
        [SerializeField] private Image iconImage;


        private InspectorViewModel<PaletteData<Preset<UnitData>>> _viewModel;
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

        private void OnUpdatePreset(PaletteData<Preset<UnitData>> preset)
        {
            nameText.text = $"Name: {preset.Name}";
            healthText.text = $"MaxHealth: {preset.Data.Data.MaxHealth.ToString(CultureInfo.InvariantCulture)}";
            iconImage.sprite = preset.Icon;
        }

        private void OnUpdateVisibility(bool isVisible)
        {
            windowGameObject.SetActive(isVisible);
        }

        private void OnDisable()
        {
            _disposable.Dispose();
            _viewModel.Dispose();
        }
    }
}