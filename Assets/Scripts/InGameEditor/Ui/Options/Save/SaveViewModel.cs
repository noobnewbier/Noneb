using Experiment.CrossPlatformLiveData;
using GameEnvironments.Common;
using GameEnvironments.Common.Repositories.CurrentGameEnvironment;
using GameEnvironments.Save;
using InGameEditor.Services;

namespace InGameEditor.Ui.Options.Save
{
    public class SaveViewModel
    {
        public readonly ILiveData<SavingResult> SavingResultEvent;
        public readonly ILiveData<bool> SavingDetailDialogVisibilityEvent;

        private readonly ISaveEnvironmentService _saveEnvironmentService;
        private readonly ICurrentGameEnvironmentRepository _currentGameEnvironmentRepository;
        private readonly IInGameEditorMessageService _messageService;

        public SaveViewModel(ISaveEnvironmentService saveEnvironmentService,
                             ICurrentGameEnvironmentRepository currentGameEnvironmentRepository,
                             IInGameEditorMessageService messageService)
        {
            _saveEnvironmentService = saveEnvironmentService;
            _saveEnvironmentService = saveEnvironmentService;
            _currentGameEnvironmentRepository = currentGameEnvironmentRepository;
            _messageService = messageService;

            SavingResultEvent = new LiveData<SavingResult>();
            SavingDetailDialogVisibilityEvent = new LiveData<bool>();
        }

        public void Save(string filename)
        {
            var saveResult = _saveEnvironmentService.TrySaveEnvironment(_currentGameEnvironmentRepository.Get(), filename);

            SavingResultEvent.PostValue(saveResult);
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