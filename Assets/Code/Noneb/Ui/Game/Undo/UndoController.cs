using Experiment.NoobAutoLinker.Core;
using Experiment.NoobUniRxPlugin;
using Noneb.Core.Game.Commands;
using Noneb.Core.Game.InGameMessages;
using UniRx;
using UnityEngine;

namespace Noneb.Ui.Game.Undo
{
    /// <summary>
    /// This is, at the time of writing, too small of a piece to warrant the whole MVVM, when need arise we can do it, but not now
    /// </summary>
    public class UndoController : MonoBehaviour
    {
        private ICommandExecutionService _commandExecutionService;
        private IInGameMessageService _inGameMessageService;

        [AutoLink] [SerializeField] private CommandExecutionServiceProvider commandExecutionServiceProvider;
        [AutoLink] [SerializeField] private InGameMessageServiceProvider inGameMessageServiceProvider;


        private void OnEnable()
        {
            _commandExecutionService = commandExecutionServiceProvider.Provide();
            _inGameMessageService = inGameMessageServiceProvider.Provide();
        }

        public void TryUndo()
        {
            if (_commandExecutionService.TryUndo(out var undoCommand))
                undoCommand.SubscribeOn(NoobSchedulers.ThreadPool).ObserveOn(NoobSchedulers.MainThread)
                    .Subscribe(_ => _inGameMessageService.PublishMessage("Undo successful"));
        }
    }
}