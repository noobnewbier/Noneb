using System;
using System.Collections.Generic;
using Experiment.CrossPlatformLiveData;
using Noneb.Core.Game.GameEnvironments.AvailableGameEnvironment;
using Noneb.Core.Game.GameEnvironments.Data;
using Noneb.Core.Game.GameState.GameEnvironments;
using UniRx;

namespace Noneb.Ui.InGameEditor.EnvironmentSelection
{
    public class SelectGameEnvironmentViewModel : IDisposable
    {
        private readonly IGameEnvironmentRepository _selectedGameEnvironmentRepository;
        private readonly IDisposable _disposable;

        public SelectGameEnvironmentViewModel(IAvailableGameEnvironmentGetRepository availableGameEnvironmentRepository,
                                              IGameEnvironmentRepository selectedGameEnvironmentRepository)
        {
            _selectedGameEnvironmentRepository = selectedGameEnvironmentRepository;
            CurrentlyInspectingGameEnvironmentLiveData = new LiveData<GameEnvironment>();
            AvailableGameEnvironmentLiveData = new LiveData<IEnumerable<GameEnvironment>>();

            _disposable = new CompositeDisposable
            {
                availableGameEnvironmentRepository
                    .GetObservableStream()
                    .SubscribeOn(Scheduler.ThreadPool)
                    .ObserveOn(Scheduler.MainThread)
                    .Subscribe(AvailableGameEnvironmentLiveData.PostValue),

                _selectedGameEnvironmentRepository
                    .GetObservableStream()
                    .SubscribeOn(Scheduler.ThreadPool)
                    .ObserveOn(Scheduler.MainThread)
                    .Subscribe(CurrentlyInspectingGameEnvironmentLiveData.PostValue)
            };
        }

        public ILiveData<GameEnvironment> CurrentlyInspectingGameEnvironmentLiveData { get; }
        public ILiveData<IEnumerable<GameEnvironment>> AvailableGameEnvironmentLiveData { get; }

        public void Dispose()
        {
            _disposable?.Dispose();
        }

        public void InspectGameEnvironment(GameEnvironment environment)
        {
            CurrentlyInspectingGameEnvironmentLiveData.PostValue(environment);
        }

        public void SelectCurrentlyInspectedGameEnvironment()
        {
            _selectedGameEnvironmentRepository.Set(CurrentlyInspectingGameEnvironmentLiveData.Value);
        }
    }
}