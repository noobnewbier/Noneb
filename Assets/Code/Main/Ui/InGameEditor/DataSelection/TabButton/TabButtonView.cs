using System;
using TMPro;
using UnityEngine;

namespace Main.Ui.InGameEditor.DataSelection.TabButton
{
    public class TabButtonView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI buttonText;

        private TabButtonViewModel _viewModel;
        private IDisposable _disposable;

        public void Init(TabButtonViewModel viewModel)
        {
            _viewModel = viewModel;

            _disposable = _viewModel.ButtonTextLiveData.Subscribe(UpdateText);
        }

        private void UpdateText(string text)
        {
            buttonText.text = text;
        }

        public void OnClick()
        {
            _viewModel.InvokeAction();
        }

        private void OnDisable()
        {
            _disposable.Dispose();
        }
    }
}