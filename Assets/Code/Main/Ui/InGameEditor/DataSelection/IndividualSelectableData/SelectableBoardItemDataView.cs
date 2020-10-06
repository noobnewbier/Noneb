using System;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using UnityUtils.Pooling;

namespace Main.Ui.InGameEditor.DataSelection.IndividualSelectableData
{
    public class SelectableBoardItemDataView : PooledMonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI nameText;

        private SelectableBoardItemDataViewModel _viewModel;
        private IDisposable _disposable;

        private void OnEnable()
        {
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

        public void Click()
        {
            _viewModel.OnClick();
        }

        private void OnDisable()
        {
            _disposable?.Dispose();
        }
    }
}