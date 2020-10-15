using Noneb.Core.Game.Common.Providers;
using Noneb.Core.Game.Coordinates;
using UnityEngine;
using UnityUtils.Constants;

namespace Noneb.Ui.Game.GameEnvironments.Load.GameObjects
{
    [CreateAssetMenu(fileName = nameof(GameObjectLoadServiceProvider), menuName = MenuName.ScriptableService + nameof(GameObjectLoadService))]
    public class GameObjectLoadServiceProvider : ScriptableObject, IObjectProvider<IGameObjectLoadService>
    {
        [SerializeField] private CoordinateServiceProvider coordinateServiceProvider;

        private IGameObjectLoadService _cache;

        public IGameObjectLoadService Provide() => _cache ?? (_cache = new GameObjectLoadService(coordinateServiceProvider.Provide()));
    }
}