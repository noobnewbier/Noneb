﻿using Main.Core.Game.Common.Providers;
using Main.Core.Game.GameEnvironments.CurrentGameEnvironments;
using UnityEngine;
using UnityUtils.Constants;

namespace Main.Core.Game.GameEnvironments.CurrentLevelDatas
{
    [CreateAssetMenu(
        fileName = nameof(CurrentLevelDataRepositoryProvider),
        menuName = MenuName.ScriptableRepository + nameof(CurrentLevelDataRepository)
    )]
    public class CurrentLevelDataRepositoryProvider : ScriptableObject, IObjectProvider<ICurrentLevelDataRepository>
    {
        [SerializeField] private CurrentGameEnvironmentRepositoryProvider currentGameEnvironmentRepositoryProvider;

        private ICurrentLevelDataRepository _cache;

        public ICurrentLevelDataRepository Provide() =>
            _cache ??
            (_cache = new CurrentLevelDataRepository(
                currentGameEnvironmentRepositoryProvider.Provide()
            ));
    }
}