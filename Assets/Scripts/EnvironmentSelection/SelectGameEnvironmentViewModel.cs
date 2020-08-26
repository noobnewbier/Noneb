using System.Collections.Generic;
using Experiment.CrossPlatformLiveData;
using GameEnvironments.Common.Data;
using GameEnvironments.Common.Repositories.AvailableGameEnvironment;
using GameEnvironments.Common.Repositories.CurrentGameEnvironment;

namespace EnvironmentSelection
{
    public class SelectGameEnvironmentViewModel
    {
        private readonly IAvailableGameEnvironmentRepository _availableGameEnvironmentRepository;
        private readonly ICurrentGameEnvironmentSetRepository _gameEnvironmentSetRepository;

        public SelectGameEnvironmentViewModel(IAvailableGameEnvironmentRepository availableGameEnvironmentRepository,
                                              ICurrentGameEnvironmentSetRepository gameEnvironmentSetRepository)
        {
            _availableGameEnvironmentRepository = availableGameEnvironmentRepository;
            _gameEnvironmentSetRepository = gameEnvironmentSetRepository;
            CurrentlyInspectingGameEnvironmentLiveData = new LiveData<GameEnvironment>();
            AvailableGameEnvironmentLiveData = new LiveData<IEnumerable<GameEnvironment>>();
        }

        public ILiveData<GameEnvironment> CurrentlyInspectingGameEnvironmentLiveData { get; }
        public ILiveData<IEnumerable<GameEnvironment>> AvailableGameEnvironmentLiveData { get; }

        public void RefreshAvailableGameEnvironmentList()
        {
            AvailableGameEnvironmentLiveData.PostValue(_availableGameEnvironmentRepository.GameEnvironments);
        }

        public void InspectGameEnvironment(GameEnvironment environment)
        {
            CurrentlyInspectingGameEnvironmentLiveData.PostValue(environment);
        }

        public void SelectCurrentlyInspectedGameEnvironment()
        {
            _gameEnvironmentSetRepository.Set(CurrentlyInspectingGameEnvironmentLiveData.Value);
        }
    }
}