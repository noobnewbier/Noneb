using Experiment.NoobAutoLinker.Core;
using UnityEngine;

namespace Noneb.Ui.Game.InGameMessage.DebugLogMessageShower
{
    public class DebugLogMessageView : MonoBehaviour
    {
        [AutoLink] [SerializeField] private DebugLogMessageViewModelFactory viewModelFactory;

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