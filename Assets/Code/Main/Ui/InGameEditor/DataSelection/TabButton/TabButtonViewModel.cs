using System;
using Experiment.CrossPlatformLiveData;

namespace Main.Ui.InGameEditor.DataSelection.TabButton
{
    public class TabButtonViewModel
    {
        public ILiveData<string> ButtonTextLiveData { get; }
        private readonly Action _clickedAction;

        public TabButtonViewModel(Action clickedAction, string buttonText)
        {
            _clickedAction = clickedAction;
            ButtonTextLiveData = new LiveData<string>();
            
            ButtonTextLiveData.PostValue(buttonText);
        }

        public void InvokeAction()
        {
            _clickedAction.Invoke();
        }
    }
}