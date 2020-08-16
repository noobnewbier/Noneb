using System;
using GameEnvironments.Common.Data.LevelDatas;
using GameEnvironments.Common.Repositories.CurrentGameEnvironment;
using UniRx;

namespace GameEnvironments.Common.Repositories.CurrentLevelData
{
    public interface ICurrentLevelDataRepository
    {
        IObservable<LevelData> Get();
    }

    public class CurrentLevelDataRepository : ICurrentLevelDataRepository
    {
        private readonly ICurrentGameEnvironmentGetRepository _gameEnvironmentGetRepository;
        
        public CurrentLevelDataRepository(ICurrentGameEnvironmentGetRepository currentGameEnvironmentGetRepository)
        {
            _gameEnvironmentGetRepository = currentGameEnvironmentGetRepository;
        }

        public IObservable<LevelData> Get()
        {
            return _gameEnvironmentGetRepository.Get().Select(d => d.LevelData);
        }
    }
}