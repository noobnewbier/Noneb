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
        [SerializeField] private TileInspectorViewModelFactory viewModelFactory;

        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI weightText;
        [SerializeField] private Image iconImage;

        private InspectorViewModel<Tile, TileData> _presetViewModel;
        private IDisposable _disposable;

        private void OnEnable()
        {
            _presetViewModel = viewModelFactory.Create();
            _disposable = new CompositeDisposable
            {
                _presetViewModel.TypeTLiveData.Subscribe(OnUpdateData),
                _presetViewModel.VisibilityLiveData.Subscribe(OnUpdateVisibility),
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