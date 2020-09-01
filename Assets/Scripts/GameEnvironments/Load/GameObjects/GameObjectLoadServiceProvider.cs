using System;
using Common.Providers;
using UnityEngine;
using UnityUtils.Constants;

namespace GameEnvironments.Load.GameObjects
{
    [CreateAssetMenu(fileName = nameof(GameObjectLoadServiceProvider), menuName = MenuName.ScriptableService + nameof(GameObjectLoadService))]
    public class GameObjectLoadServiceProvider : ScriptableObjectProvider<IGameObjectLoadService>
    {
        private readonly Lazy<IGameObjectLoadService> _lazyInstance = new Lazy<IGameObjectLoadService>(() => new GameObjectLoadService());

        public override IGameObjectLoadService Provide()
        {
            return _lazyInstance.Value;
        }
    }
}