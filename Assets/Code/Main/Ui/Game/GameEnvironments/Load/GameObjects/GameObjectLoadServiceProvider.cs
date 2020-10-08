﻿using Main.Core.Game.Common.Providers;
using Main.Core.Game.Coordinates;
using UnityEngine;
using UnityUtils.Constants;

namespace Main.Ui.Game.GameEnvironments.Load.GameObjects
{
    [CreateAssetMenu(fileName = nameof(GameObjectLoadServiceProvider), menuName = MenuName.ScriptableService + nameof(GameObjectLoadService))]
    public class GameObjectLoadServiceProvider : ScriptableObject, IObjectProvider<IGameObjectLoadService>
    {
        [SerializeField] private CoordinateServiceProvider coordinateServiceProvider;

        private IGameObjectLoadService _cache;

        public IGameObjectLoadService Provide() => _cache ?? (_cache = new GameObjectLoadService(coordinateServiceProvider.Provide()));
    }
}