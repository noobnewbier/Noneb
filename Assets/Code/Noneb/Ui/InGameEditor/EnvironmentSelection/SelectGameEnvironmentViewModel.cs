using System;
using System.Collections.Generic;
using Experiment.CrossPlatformLiveData;
using Noneb.Core.Game.GameEnvironments.AvailableGameEnvironment;
using Noneb.Core.Game.GameEnvironments.Data;
using Noneb.Core.Game.GameState.CurrentGameEnvironments;
using UniRx;

namespace Noneb.Ui.InGameEditor.EnvironmentSelection
{
    public class SelectGameEnvironmentViewModel : IDisposable
    {
        private readonly ICurrentGameEnvironmentRepository _currentGameEnvironmentRepository;
        private readonly IDisposable _disposable;

        public SelectGameEnvironmentViewModel(IAvailableGameEnvironmentGetRepository availableGameEnvironmentRepository,
                                              ICurrentGameEnvironmentRepository currentGameEnvironmentRepository)
        {
            _currentGameEnvironmentRepository = currentGameEnvironmentRepository;
            CurrentlyInspectingGameEnvironmentLiveData = new LiveData<GameEnvironment>();
            AvailableGameEnvironmentLiveData = new LiveData<IEnumerable<GameEnvironment>>();

            _disposable = new CompositeDisposable
            {
                availableGameEnvironmentRepository
                    .GetObservableStream()
                    .SubscribeOn(Scheduler.ThreadPool)
                    .ObserveOn(Scheduler.MainThread)
                    .Subscribe(AvailableGameEnvironmentLiveData.PostValue),

                _currentGameEnvironmentRepository
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
            _currentGameEnvironmentRepository.Set(CurrentlyInspectingGameEnvironmentLiveData.Value);
        }
    }
}