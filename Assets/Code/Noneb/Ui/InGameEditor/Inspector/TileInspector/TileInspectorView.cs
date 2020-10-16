using System;
using System.Globalization;
using Noneb.Core.Game.Tiles;
using Noneb.Core.InGameEditor.Data;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Noneb.Ui.InGameEditor.Inspector.TileInspector
{
    public class TileInspectorView : MonoBehaviour
    {
        [SerializeField] private GameObject windowGameObject;
        
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI weightText;
        [SerializeField] private Image iconImage;

        [SerializeField] private InspectorViewModelFactory viewModelFactory;

        private InspectorViewModel<PaletteData<Preset<TileData>>> _viewModel;
        private IDisposable _disposable;

        private void OnEnable()
        {
            _viewModel = viewModelFactory.Create();
            _disposable = new CompositeDisposable
            {
                _viewModel.TilePresetPaletteData.Subscribe(OnUpdatePreset),
                _viewModel.VisibilityLiveData.Subscribe(OnUpdateVisibility)
            };
        }

        private void OnDisable()
        {
            _disposable.Dispose();
            _viewModel.Dispose();
        }

        private void OnUpdatePreset(PaletteData<Preset<TileData>> preset)
        {
            nameText.text = $"Name: {preset.Name}";
            weightText.text = $"Weight: {preset.Data.Data.Weight.ToString(CultureInfo.InvariantCulture)}";
            iconImage.sprite = preset.Icon;
        }

        private void OnUpdateVisibility(bool isVisible)
        {
            windowGameObject.SetActive(isVisible);
        }
    }
}