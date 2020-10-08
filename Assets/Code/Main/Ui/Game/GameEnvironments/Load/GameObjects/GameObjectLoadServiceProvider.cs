using Main.Core.Game.Common.Providers;
using Main.Core.Game.Maps.Coordinate;
using UnityEngine;
using UnityUtils.Constants;

namespace Main.Ui.Game.GameEnvironments.Load.GameObjects
{
    [CreateAssetMenu(fileName = nameof(GameObjectLoadServiceProvider), menuName = MenuName.ScriptableService + nameof(GameObjectLoadService))]
    public class GameObjectLoadServiceProvider : ScriptableObjectProvider<IGameObjectLoadService>
    {
        [SerializeField] private CoordinateServiceProvider coordinateServiceProvider;

        private IGameObjectLoadService _cache;

        public override IGameObjectLoadService Provide() => _cache ?? (_cache = new GameObjectLoadService(coordinateServiceProvider.Provide()));
    }
}