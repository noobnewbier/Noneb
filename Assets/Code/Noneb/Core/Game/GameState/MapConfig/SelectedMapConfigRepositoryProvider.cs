﻿using Noneb.Core.Game.Common.Providers;
using Noneb.Core.Game.GameState.CurrentGameEnvironments;
using UnityEngine;
using UnityUtils.Constants;

namespace Noneb.Core.Game.GameState.CurrentMapConfig
{
    [CreateAssetMenu(
        fileName = nameof(SelectedMapConfigRepositoryProvider),
        menuName = MenuName.ScriptableRepository + nameof(MapConfigRepository)
    )]
    public class SelectedMapConfigRepositoryProvider : ScriptableObject, IObjectProvider<IMapConfigRepository>
    {
        [SerializeField] private SelectedGameEnvironmentRepositoryProvider gameEnvironmentRepositoryProvider;

        private IMapConfigRepository _cache;

        public IMapConfigRepository Provide() =>
            _cache ?? (_cache = new MapConfigRepository(gameEnvironmentRepositoryProvider.Provide()));
    }
}