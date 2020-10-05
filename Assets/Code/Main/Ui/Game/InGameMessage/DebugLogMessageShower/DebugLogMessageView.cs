using UnityEngine;

namespace InGameEditor.Ui.EditorMessageShower
{
    public class DebugLogMessageView : MonoBehaviour
    {
        [SerializeField] private DebugLogMessageViewModelFactory viewModelFactory;

        private IDebugLogMessageViewModel _viewModel;

        private void OnEnable()
        {
            _viewModel = viewModelFactory.Create();

            _viewModel.MessageLiveData.Subscribe(ShowMessage);
        }

        private static void ShowMessage(string message)
        {
            Debug.Log(message);
        }

        private void OnDisable()
        {
            _viewModel.Dispose();
        }
    }
}