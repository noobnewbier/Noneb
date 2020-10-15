using System;
using Experiment.CrossPlatformLiveData;

namespace Noneb.Ui.InGameEditor.DataSelection.TabButton
{
    public class TabButtonViewModel
    {
        private readonly Action _clickedAction;

        public TabButtonViewModel(Action clickedAction, string buttonText)
        {
            _clickedAction = clickedAction;
            ButtonTextLiveData = new LiveData<string>();

            ButtonTextLiveData.PostValue(buttonText);
        }

        public ILiveData<string> ButtonTextLiveData { get; }

        public void InvokeAction()
        {
            _clickedAction.Invoke();
        }
    }
}