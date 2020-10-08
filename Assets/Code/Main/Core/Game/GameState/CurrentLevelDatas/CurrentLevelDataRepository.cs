using System;
using Main.Core.Game.Common;
using Main.Core.Game.GameEnvironments.Data.LevelDatas;
using Main.Core.Game.GameState.CurrentGameEnvironments;
using UniRx;

namespace Main.Core.Game.GameState.CurrentLevelDatas
{
    public interface ICurrentLevelDataRepository : IDataGetRepository<LevelData>
    {
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