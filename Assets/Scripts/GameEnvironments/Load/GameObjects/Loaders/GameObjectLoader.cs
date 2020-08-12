using System.Collections.Immutable;
using Common.Providers;
using GameEnvironments.Common.Repositories.CurrentLevelData;
using Maps;
using Tiles;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameEnvironments.Load.GameObjects.Loaders
{
    public abstract class GameObjectLoader : MonoBehaviour
    {
        [SerializeField] private GameObjectLoadServiceProvider serviceProvider;

        [FormerlySerializedAs("levelDataRepositoryProvider")] [SerializeField]
        private CurrentLevelDataRepositoryProvider currentLevelDataRepositoryProvider;

        [SerializeField] private TilesTransformProvider tilesTransformProvider;
        [SerializeField] private MapConfigurationProvider mapConfigProvider;


        [ContextMenu(nameof(Load))]
        public void Load()
        {
            var gameObjectLoadService = serviceProvider.Provide();
            var levelDataRepository = currentLevelDataRepositoryProvider.Provide();
            var mapConfig = mapConfigProvider.Provide();

            gameObjectLoadService.Load(
                GetGameObjectProvidersFromRepository(levelDataRepository),
                tilesTransformProvider.Provide(),
                mapConfig.GetMap2DActualWidth(),
                mapConfig.GetMap2DActualHeight()
            );
        }

        protected abstract ImmutableArray<GameObjectProvider> GetGameObjectProvidersFromRepository(
            ICurrentLevelDataRepository currentLevelDataRepository);
    }
}