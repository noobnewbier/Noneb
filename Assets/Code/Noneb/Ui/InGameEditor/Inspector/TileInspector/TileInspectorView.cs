using System;
using System.Globalization;
using Noneb.Core.Game.Tiles;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Noneb.Ui.InGameEditor.Inspector.TileInspector
{
    public class TileInspectorView : MonoBehaviour
    {
        private IDisposable _disposable;

        private BoardItemInspectorViewModel<Tile, TileData> _presetViewModel;
        [SerializeField] private Image iconImage;

        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TileInspectorViewModelFactory viewModelFactory;
        [SerializeField] private TextMeshProUGUI weightText;
        [SerializeField] private GameObject windowGameObject;

        private void OnEnable()
        {
            _presetViewModel = viewModelFactory.Create();
            _disposable = new CompositeDisposable
            {
                _presetViewModel.TypeTLiveData.Subscribe(OnUpdateData),
                _presetViewModel.VisibilityLiveData.Subscribe(OnUpdateVisibility)
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