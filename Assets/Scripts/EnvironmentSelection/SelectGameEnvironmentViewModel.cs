using System.Collections.Generic;
using Experiment.CrossPlatformLiveData;
using GameEnvironments.Common.Data;
using GameEnvironments.Common.Repositories.AvailableGameEnvironment;

namespace EnvironmentSelection
{
    public interface ISelectGameEnvironmentViewModel
    {
        ILiveData<GameEnvironment> CurrentlyInspectingGameEnvironmentLiveData { get; }
        ILiveData<IEnumerable<GameEnvironment>> AvailableGameEnvironmentLiveData { get; }
        void RefreshAvailableGameEnvironmentList();
        void InspectGameEnvironment(GameEnvironment environment);
        void SelectCurrentlyInspectedGameEnvironment();
    }

    public class SelectGameEnvironmentViewModel : ISelectGameEnvironmentViewModel
    {
        private readonly IAvailableGameEnvironmentRepository _availableGameEnvironmentRepository;
        private readonly RuntimeSelectedGameEnvironment _runtimeSelectedGameEnvironment;

        public SelectGameEnvironmentViewModel(IAvailableGameEnvironmentRepository availableGameEnvironmentRepository, RuntimeSelectedGameEnvironment runtimeSelectedGameEnvironment)
        {
            _availableGameEnvironmentRepository = availableGameEnvironmentRepository;
            _runtimeSelectedGameEnvironment = runtimeSelectedGameEnvironment;
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
            _runtimeSelectedGameEnvironment.CurrentReference = CurrentlyInspectingGameEnvironmentLiveData.Value;
        }
    }
}