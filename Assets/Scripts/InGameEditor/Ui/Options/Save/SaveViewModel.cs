using Experiment.CrossPlatformLiveData;
using InGameEditor.Data;
using InGameEditor.Repositories.GameEnvironments;
using InGameEditor.Services.SaveEnvironment;
using UniRx;

namespace InGameEditor.Ui.Options.Save
{
    public class SaveViewModel
    {
        public readonly ILiveData<SavingResult> SavingResultEvent;
        public readonly ILiveData<bool> SavingDetailDialogVisibilityEvent;

        private readonly ISaveEnvironmentAsPreservationService _saveEnvironmentAsPreservationService;
        private readonly ICurrentGameEnvironmentRepository _currentGameEnvironmentRepository;
        private readonly EditorPalette _palette;

        public SaveViewModel(ISaveEnvironmentAsPreservationService saveEnvironmentAsPreservationService,
                             EditorPalette palette,
                             ICurrentGameEnvironmentRepository currentGameEnvironmentRepository)
        {
            _saveEnvironmentAsPreservationService = saveEnvironmentAsPreservationService;
            _palette = palette;
            _currentGameEnvironmentRepository = currentGameEnvironmentRepository;

            SavingResultEvent = new LiveData<SavingResult>();
            SavingDetailDialogVisibilityEvent = new LiveData<bool>();
        }

        public void Save(string filename)
        {
            var saveResult = _saveEnvironmentAsPreservationService.TrySaveEnvironment(_currentGameEnvironmentRepository.Get(), _palette, filename);

            SavingResultEvent.PostValue(saveResult);
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