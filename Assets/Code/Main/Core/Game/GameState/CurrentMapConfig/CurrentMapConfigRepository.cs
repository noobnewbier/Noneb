﻿using System;
using Main.Core.Game.Common;
using Main.Core.Game.GameState.CurrentGameEnvironments;
using Main.Core.Game.Maps;
using UniRx;

namespace Main.Core.Game.GameState.CurrentMapConfig
{
    public interface ICurrentMapConfigRepository : IDataGetRepository<MapConfig>
    {
    }

    public class CurrentMapConfigRepository : ICurrentMapConfigRepository
    {
        private readonly ICurrentGameEnvironmentGetRepository _gameEnvironmentGetRepository;

        public CurrentMapConfigRepository(ICurrentGameEnvironmentGetRepository gameEnvironmentGetRepository)
        {
            _gameEnvironmentGetRepository = gameEnvironmentGetRepository;
        }

        public IObservable<MapConfig> GetObservableStream()
        {
            return _gameEnvironmentGetRepository.GetObservableStream().Select(d => d.MapConfiguration);
        }

        public IObservable<MapConfig> GetMostRecent()
        {
            return _gameEnvironmentGetRepository.GetMostRecent().Select(d => d.MapConfiguration);
        }
    }
}