using Common.Providers;
using GameEnvironments.Common.Data;
using GameEnvironments.Common.Repositories.CurrentGameEnvironment;
using Tiles;
using UnityEngine;

namespace GameEnvironments.Load.GameObjects.Loaders
{
    public abstract class GameObjectLoader : MonoBehaviour
    {
        [SerializeField] private CurrentGameEnvironmentRepositoryProvider gameEnvironmentRepositoryProvider;
        [SerializeField] private GameObjectLoadServiceProvider serviceProvider;
        [SerializeField] private TilesTransformProvider tilesTransformProvider;

        private IGameObjectLoadService _gameObjectLoadService;
        private ICurrentGameEnvironmentRepository _currentGameEnvironmentRepository;

        private void OnEnable()
        {
            _gameObjectLoadService = serviceProvider.Provide();
            _currentGameEnvironmentRepository = gameEnvironmentRepositoryProvider.Provide();
        }

        [ContextMenu(nameof(Load))]
        public void Load()
        {
            var gameEnvironment = _currentGameEnvironmentRepository.Get();
            var mapConfig = gameEnvironment.MapConfiguration;
            
            _gameObjectLoadService.Load(
                GetGameObjectProvidersFromGameEnvironment(gameEnvironment),
                tilesTransformProvider.Provide(),
                mapConfig.XSize,
                mapConfig.ZSize
            );
        }

        protected abstract GameObjectProvider[] GetGameObjectProvidersFromGameEnvironment(GameEnvironment environment);
    }
}