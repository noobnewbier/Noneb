using System.Collections.Generic;
using Experiment.CrossPlatformLiveData;
using GameEnvironments.Common.Data;
using GameEnvironments.Common.Repositories.AvailableGameEnvironment;

namespace EnvironmentSelection
{
    public interface ISelectGameEnvironmentViewModel
    {
        ILiveData<GameEnvironment> SelectedGameEnvironmentLiveData { get; }
        ILiveData<IEnumerable<GameEnvironment>> AvailableGameEnvironmentLiveData { get; }
        void RefreshAvailableGameEnvironmentList();
        void SelectGameEnvironment(GameEnvironment environment);
    }

    public class SelectGameEnvironmentViewModel : ISelectGameEnvironmentViewModel
    {
        private readonly IAvailableGameEnvironmentRepository _availableGameEnvironmentRepository;

        public SelectGameEnvironmentViewModel(IAvailableGameEnvironmentRepository availableGameEnvironmentRepository)
        {
            _availableGameEnvironmentRepository = availableGameEnvironmentRepository;
            SelectedGameEnvironmentLiveData = new LiveData<GameEnvironment>();
            AvailableGameEnvironmentLiveData = new LiveData<IEnumerable<GameEnvironment>>();
        }

        public ILiveData<GameEnvironment> SelectedGameEnvironmentLiveData { get; }
        public ILiveData<IEnumerable<GameEnvironment>> AvailableGameEnvironmentLiveData { get; }

        public void RefreshAvailableGameEnvironmentList()
        {
            AvailableGameEnvironmentLiveData.PostValue(_availableGameEnvironmentRepository.GameEnvironments);
        }

        public void SelectGameEnvironment(GameEnvironment environment)
        {
            SelectedGameEnvironmentLiveData.PostValue(environment);
        }
    }
}