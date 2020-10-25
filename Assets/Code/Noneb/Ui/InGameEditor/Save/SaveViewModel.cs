using System;
using Experiment.CrossPlatformLiveData;
using Noneb.Core.Game.GameEnvironments.Save;
using Noneb.Core.Game.GameState.CurrentGameEnvironments;
using Noneb.Core.Game.InGameMessages;
using Noneb.Core.InGameEditor.Save;
using UniRx;

namespace Noneb.Ui.InGameEditor.Save
{
    public class SaveViewModel : IDisposable
    {
        public readonly ILiveData<SavingResult> savingResultEvent;
        public readonly ILiveData<bool> savingDetailDialogVisibilityEvent;

        private readonly ISaveEnvironmentService _saveEnvironmentService;
        private readonly IGameEnvironmentGetRepository _loadedGameEnvironmentGetRepository;
        private readonly IInGameMessageService _messageService;

        private IDisposable _disposable;

        public SaveViewModel(ISaveEnvironmentService saveEnvironmentService,
                             IGameEnvironmentGetRepository loadedGameEnvironmentGetRepository,
                             IInGameMessageService messageService)
        {
            _saveEnvironmentService = saveEnvironmentService;
            _saveEnvironmentService = saveEnvironmentService;
            _loadedGameEnvironmentGetRepository = loadedGameEnvironmentGetRepository;
            _messageService = messageService;

            savingResultEvent = new LiveData<SavingResult>();
            savingDetailDialogVisibilityEvent = new LiveData<bool>();
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }

        public void Save(string filename)
        {
            _disposable = _loadedGameEnvironmentGetRepository
                .GetMostRecent()
                .SubscribeOn(Scheduler.ThreadPool)
                .ObserveOn(Scheduler.MainThread)
                .Subscribe(
                    env =>
                    {
                        var saveResult = _saveEnvironmentService.TrySaveEnvironment(env, filename);
                        savingResultEvent.PostValue(saveResult);
                    }
                );
        }

        public void ShowEditorMessage(string message)
        {
            _messageService.PublishMessage(message);
        }

        public void ClickedSaveMenuButton()
        {
            savingDetailDialogVisibilityEvent.PostValue(true);
        }

        public void ClickedCancelInSavingDetail()
        {
            savingDetailDialogVisibilityEvent.PostValue(false);
        }
    }
}