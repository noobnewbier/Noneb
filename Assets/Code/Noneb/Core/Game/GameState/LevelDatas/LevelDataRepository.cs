using System;
using Noneb.Core.Game.Common;
using Noneb.Core.Game.GameEnvironments.Data.LevelDatas;
using Noneb.Core.Game.GameState.CurrentGameEnvironments;
using UniRx;

namespace Noneb.Core.Game.GameState.CurrentLevelDatas
{
    public interface ILevelDataRepository : IDataGetRepository<LevelData>
    {
    }

    public class LevelDataRepository : ILevelDataRepository
    {
        private readonly IGameEnvironmentGetRepository _gameEnvironmentGetRepository;

        public LevelDataRepository(IGameEnvironmentGetRepository gameEnvironmentGetRepository)
        {
            _gameEnvironmentGetRepository = gameEnvironmentGetRepository;
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