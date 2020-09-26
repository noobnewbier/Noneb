using System;
using Common.Providers;
using Maps.Services;
using UnityEngine;
using UnityUtils.Constants;

namespace GameEnvironments.Load.GameObjects
{
    [CreateAssetMenu(fileName = nameof(GameObjectLoadServiceProvider), menuName = MenuName.ScriptableService + nameof(GameObjectLoadService))]
    public class GameObjectLoadServiceProvider : ScriptableObjectProvider<IGameObjectLoadService>
    {
        [SerializeField] private CoordinateServiceProvider coordinateServiceProvider;

        private IGameObjectLoadService _cache;

        public override IGameObjectLoadService Provide()
        {
            return _cache?? (_cache = new GameObjectLoadService(coordinateServiceProvider.Provide()));
        }
    }
}