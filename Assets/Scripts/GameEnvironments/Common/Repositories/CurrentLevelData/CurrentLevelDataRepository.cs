using System;
using GameEnvironments.Common.Data.LevelDatas;
using GameEnvironments.Common.Repositories.CurrentGameEnvironment;
using UniRx;

namespace GameEnvironments.Common.Repositories.CurrentLevelData
{
    public interface ICurrentLevelDataRepository
    {
        IObservable<LevelData> GetObservableStream();
        IObservable<LevelData> GetMostRecent();
    }

    public class CurrentLevelDataRepository : ICurrentLevelDataRepository
    {
        private readonly ICurrentGameEnvironmentGetRepository _gameEnvironmentGetRepository;
        
        public CurrentLevelDataRepository(ICurrentGameEnvironmentGetRepository currentGameEnvironmentGetRepository)
        {
            _gameEnvironmentGetRepository = currentGameEnvironmentGetRepository;
        }

        public IObservable<LevelData> GetObservableStream()
        {
            return _gameEnvironmentGetRepository.GetObservableStream().Select(d => d.LevelData);
        }

        public IObservable<LevelData> GetMostRecent()
        {
            return _gameEnvironmentGetRepository.GetMostRecent().Select(d => d.LevelData);
        }
    }
}