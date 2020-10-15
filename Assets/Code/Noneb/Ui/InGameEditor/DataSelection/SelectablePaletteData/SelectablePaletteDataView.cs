using System;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using UnityUtils.Pooling;

namespace Noneb.Ui.InGameEditor.DataSelection.SelectablePaletteData
{
    public class SelectablePaletteDataView : PooledMonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI nameText;

        private SelectablePaletteDataViewModel _viewModel;
        private IDisposable _disposable;

        public void Init(SelectablePaletteDataViewModel viewModel)
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