using System;
using System.Globalization;
using Noneb.Core.Game.Units;
using Noneb.Core.InGameEditor.Data;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Unit = Noneb.Core.Game.Units.Unit;

namespace Noneb.Ui.InGameEditor.Inspector.UnitInspector
{
    public class UnitInspectorView : MonoBehaviour
    {
        [SerializeField] private GameObject windowGameObject;

        [SerializeField] private UnitInspectorViewModelFactory viewModelFactory;
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI healthText;
        [SerializeField] private Image iconImage;


        private InspectorViewModel<Unit, UnitData> _viewModel;
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

        private void OnUpdatePreset(UnitData unitData)
        {
            nameText.text = $"Name: {unitData.Name}";
            healthText.text = $"MaxHealth: {unitData.MaxHealth.ToString(CultureInfo.InvariantCulture)}";
            iconImage.sprite = unitData.Icon;
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