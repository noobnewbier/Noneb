using System;
using Noneb.Core.InGameEditor.Data;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityUtils.Pooling;

namespace Noneb.Ui.InGameEditor.DataSelection.SelectablePaletteData
{
    public class SelectablePaletteDataView : PooledMonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI nameText;

        private SelectablePaletteDataViewModel<PaletteData> _viewModel;
        private IDisposable _disposable;

        public void OnPointerClick(PointerEventData eventData)
        {
            _viewModel.Inspect();
        }

        public void Init(SelectablePaletteDataViewModel<PaletteData> viewModel)
        {
            _viewModel = viewModel;
            _disposable = new CompositeDisposable
            {
                _viewModel.BoardItemUiInfoLiveData.Subscribe(tuple => SetUpImageWithData(tuple.sprite, tuple.dataName))
            };
        }

        private void SetUpImageWithData(Sprite sprite, string dataName)
        {
            icon.sprite = sprite;
            nameText.text = dataName;
        }

        private void OnDisable()
        {
            _disposable?.Dispose();
        }
    }
}