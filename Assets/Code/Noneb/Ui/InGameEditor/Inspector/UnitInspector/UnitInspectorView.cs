using System;
using System.Globalization;
using Noneb.Core.Game.Units;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Noneb.Ui.InGameEditor.Inspector.UnitInspector
{
    public class UnitInspectorView : MonoBehaviour
    {
        private IDisposable _disposable;


        private UnitInspectorViewModel _viewModel;
        [SerializeField] private TextMeshProUGUI healthText;
        [SerializeField] private Image iconImage;
        [SerializeField] private TextMeshProUGUI nameText;

        [SerializeField] private UnitInspectorViewModelFactory viewModelFactory;
        [SerializeField] private GameObject windowGameObject;

        private void OnEnable()
        {
            _viewModel = viewModelFactory.Create();
            _disposable = new CompositeDisposable
            {
                _viewModel.UnitDataLiveData.Subscribe(OnUpdatePreset),
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