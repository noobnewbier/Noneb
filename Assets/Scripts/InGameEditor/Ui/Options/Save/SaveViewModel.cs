using System;
using Experiment.CrossPlatformLiveData;
using GameEnvironments.Common;
using GameEnvironments.Common.Repositories.CurrentGameEnvironments;
using GameEnvironments.Save;
using InGameEditor.Services.InGameEditorMessage;
using UniRx;

namespace InGameEditor.Ui.Options.Save
{
    public class SaveViewModel : IDisposable
    {
        public readonly ILiveData<SavingResult> SavingResultEvent;
        public readonly ILiveData<bool> SavingDetailDialogVisibilityEvent;

        private readonly ISaveEnvironmentService _saveEnvironmentService;
        private readonly ICurrentGameEnvironmentGetRepository _currentGameEnvironmentGetRepository;
        private readonly IInGameEditorMessageService _messageService;

        private IDisposable _disposable;

        public SaveViewModel(ISaveEnvironmentService saveEnvironmentService,
                             ICurrentGameEnvironmentGetRepository currentGameEnvironmentGetRepository,
                             IInGameEditorMessageService messageService)
        {
            _saveEnvironmentService = saveEnvironmentService;
            _saveEnvironmentService = saveEnvironmentService;
            _currentGameEnvironmentGetRepository = currentGameEnvironmentGetRepository;
            _messageService = messageService;

            SavingResultEvent = new LiveData<SavingResult>();
            SavingDetailDialogVisibilityEvent = new LiveData<bool>();
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }

        public void Save(string filename)
        {
            _disposable = _currentGameEnvironmentGetRepository.GetMostRecent()
                .Subscribe(
                    env =>
                    {
                        var saveResult = _saveEnvironmentService.TrySaveEnvironment(env, filename);
                        SavingResultEvent.PostValue(saveResult);
                    }
                );
        }

        public void ShowEditorMessage(string message)
        {
            _messageService.PublishMessage(message);
        }

        public void ClickedSaveMenuButton()
        {
            SavingDetailDialogVisibilityEvent.PostValue(true);
        }

        public void ClickedCancelInSavingDetail()
        {
            SavingDetailDialogVisibilityEvent.PostValue(false);
        }
    }
}