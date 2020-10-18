using System;
using System.Globalization;
using Noneb.Core.Game.Tiles;
using Noneb.Core.InGameEditor.Data;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Noneb.Ui.InGameEditor.Inspector.TileInspector
{
    public class TileInspectorView : MonoBehaviour
    {
        [SerializeField] private GameObject windowGameObject;
        
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI weightText;
        [SerializeField] private Image iconImage;

        [FormerlySerializedAs("viewModelFactory")] [SerializeField] private TilePresetInspectorViewModelFactory presetViewModelFactory;
        [SerializeField] private CoordinateTileInspectorViewModelFactory coordinateTileInspectorViewModelFactory;
        

        private PresetPaletteInspectorViewModel<PaletteData<Preset<TileData>>, TileData> _presetViewModel;
        private CoordinateInspectorViewModel<Tile, TileData> _coordinateInspectorViewModel;
        private IDisposable _disposable;

        private void OnEnable()
        {
            _presetViewModel = presetViewModelFactory.Create();
            _coordinateInspectorViewModel = coordinateTileInspectorViewModelFactory.Create();
            _disposable = new CompositeDisposable
            {
                _presetViewModel.TypeTLiveData.Subscribe(OnUpdateData),
                _presetViewModel.VisibilityLiveData.Subscribe(OnUpdateVisibility),
                _coordinateInspectorViewModel.TypeTLiveData.Subscribe(OnUpdateData),
                _coordinateInspectorViewModel.VisibilityLiveData.Subscribe(OnUpdateVisibility)
            };
        }

        private void OnDisable()
        {
            _disposable.Dispose();
            _presetViewModel.Dispose();
        }

        private void OnUpdateData(TileData data)
        {
            nameText.text = $"Name: {data.Name}";
            weightText.text = $"Weight: {data.Weight.ToString(CultureInfo.InvariantCulture)}";
            iconImage.sprite = data.Icon;
        }

        private void OnUpdateVisibility(bool isVisible)
        {
            windowGameObject.SetActive(isVisible);
        }
    }
}